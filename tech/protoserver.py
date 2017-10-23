import os
import sys

msg_dir = "./msg/proto"
msgid_conf = "./msg/msgid.conf"

def compile_proto(proto_dir):
	files = os.listdir(proto_dir)
	for f in files:
		print(f)
		if f.endswith('.proto'):
			target_file = f.replace(".proto",".pb")
			os.system(".\\tools\\protoc.exe -o../server/proto/"+target_file+" ./msg/proto/"+f)

compile_proto(msg_dir)

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
			comment = "//" + array_info[1].strip().rstrip()
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
		