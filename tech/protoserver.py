import os
import sys
import shutil

msgid_conf = "./msg/msgid.conf"
msg_dir = "./thrift/"

def compile_proto(proto_dir,target_dir):
	files = os.listdir(proto_dir)
	for f in files:
		if not f.endswith('.thrift'):
			continue
		print("proto_dir " + proto_dir + f)
		os.system(".\\thrift\\thrift.exe " + "--gen lua " + proto_dir + f)

def  move_script(script_dir, target_dir):
	files = os.listdir(script_dir)
	for f in files:
		if os.path.isdir(script_dir + f+"/"):
			move_script(script_dir + f+"/", target_dir)
			continue
		if f.endswith(".lua"):
			print(f)
			shutil.move(script_dir + f, target_dir + f)
		
def get_targetDir():
	curPath = os.getcwd()
	return curPath.replace("tech","server/proto/")

target_dir = get_targetDir()
compile_proto(msg_dir, target_dir)
move_script(os.getcwd() + "/", target_dir)
def ParseMsgIDDefine(fs,msgidList):
	fs.writelines("local message = {}");
	fs.writelines("");

	for _msgDef in msgidList:
		msgname = _msgDef.msgname
		for i in range(60-len(msgname)):
			msgname += " "
		fs.writelines("message.%s \t= %s; %s"%( msgname.upper().replace(".","_"), _msgDef.msgid,_msgDef.comment));

	fs.writelines("");
	fs.writelines("return message");
	fs.flush();
	fs.close();

class MsgInfo(object):
	def __init__(self,msgid,msgname,comment):
		self.msgid = msgid
		self.msgname = msgname
		self.comment = comment
		
	msgid = ""
	msgname = ""
	comment = ""

def parse_msgfile(msgid_conf):
	msg_info_list = []
	msg_file = open(msgid_conf,"r")
	for line in msg_file.readlines():
		line = line.strip().rstrip()

		if not len(line) or line.startswith("#"):
			continue
		array_info = line.split("=")
		msgid = array_info[0].strip().rstrip()	#MSGID
		
		array_info = array_info[1].split('#')	#MSGNAME
		msgname = array_info[0].strip().rstrip()
			
		comment = ""
		if len(array_info) > 1:
			comment = "--" + array_info[1].strip().rstrip()
		msg_info_list.append(MsgInfo(msgid,msgname,comment))
		
	return msg_info_list
	
class WrapFile:
	fs = None
	def __init__(self,real_file):
		self.fs = real_file
	def writelines(self,s):
		self.fs.write(s + "\n")
	def flush(self):
		self.fs.flush()
	def close(self):
		self.fs.close()
	
l=parse_msgfile(msgid_conf)

targetPath = "../server/common/message.lua";

f = WrapFile(open(targetPath,"w+"))
ParseMsgIDDefine(f,l)
		