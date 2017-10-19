import sys
import os

msg_dir = "./msg/proto/"
msgid_conf = "./msg/msgid.conf"
msg_namespace = "Monster.Protocol"

target_dir = "../../../client/Trunk/Assets/Scripts/BaseSystem/Net/Message/Protocol/"


def compile_proto(proto_dir,target_dir):
	files = os.listdir(proto_dir)
	for f in files:
		if not f.endswith('.proto'):
			continue
		target_file = f.replace(".proto",".cs")
		os.system(".\\tools\\protogen.exe -w:" + proto_dir + " -i:" + f + " -o:" + target_dir + target_file)
		print(target_file)


compile_proto(msg_dir, target_dir)

class MsgInfo(object):
	def __init__(self,msgid,msgname,comment):
		self.msgid = msgid
		self.msgname = msgname
		self.comment = comment
		
	msgid = ""
	msgname = ""
	comment = ""
	
def ParseMsgIDDefineDic(fs,msgidList):
	fs.writelines("using System;");
	fs.writelines("using System.Collections.Generic;");
	fs.writelines("using System.Text;");
	fs.writelines("using "+msg_namespace+";");
	fs.writelines("public class MsgIDDefineDic");
	fs.writelines("{");

	fs.writelines("\tprivate Dictionary<int, Type> id2msgMap = new Dictionary<int, Type>();");
	fs.writelines("\tprivate Dictionary<Type, int> msg2idMap = new Dictionary<Type, int>();");
	fs.writelines("\tprivate static MsgIDDefineDic instance;");
	fs.writelines("\tpublic static MsgIDDefineDic Instance()");

	fs.writelines("\t{");

	fs.writelines("\t\tif (null == instance)");
	fs.writelines("\t\t{");
	fs.writelines("\t\t\tinstance = new MsgIDDefineDic();");
	fs.writelines("\t\t}");
	fs.writelines("\t\treturn instance;");
	fs.writelines("\t}");


	fs.writelines("\tprivate MsgIDDefineDic()");
	fs.writelines("\t{");
	for _msgDef in msgidList:
		tmpMsgName = _msgDef.msgname;
		fs.writelines("\t\tid2msgMap.Add(%s, typeof(%s));"% (_msgDef.msgid, tmpMsgName));
		fs.writelines("\t\tmsg2idMap.Add(typeof(%s), %s);"% (tmpMsgName, _msgDef.msgid));

	fs.writelines("\t}");
	fs.writelines("\tpublic Type GetMsgType(int msgID)");
	fs.writelines("\t{");
	fs.writelines("\t\tType msgType = null;");
	fs.writelines("\t\tid2msgMap.TryGetValue(msgID, out msgType);");
	fs.writelines("\t\tif (msgType==null)");
	fs.writelines("\t\t{");
	fs.writelines("\t\t\treturn null;");
	fs.writelines("\t\t}");
	fs.writelines("\t\treturn msgType;");
	fs.writelines("\t}");

	fs.writelines("\tpublic int GetMsgID(Type type)");
	fs.writelines("\t{");
	fs.writelines("\t\tint msgID = 0;");
	fs.writelines("\t\tmsg2idMap.TryGetValue(type, out msgID);");
	fs.writelines("\t\treturn msgID;");
	fs.writelines("\t}");
	fs.writelines("}");

	fs.flush();
	fs.close();

def ParseMsgIDDefine(fs,msgidList):
	fs.writelines("using System;");
	fs.writelines("using System.Collections.Generic;");
	fs.writelines("using System.Text;");
	fs.writelines("using "+msg_namespace+";");
	fs.writelines("public class MsgIDDefine");
	fs.writelines("{");

	for _msgDef in msgidList:
		fs.writelines("\tpublic const int %s = %s; %s"%( _msgDef.msgname.replace(".","_"), _msgDef.msgid,_msgDef.comment));

	fs.writelines("}");
	fs.flush();
	fs.close();

def parse_msgfile(msgid_conf):
	msg_info_list = []
	msg_file = open(msgid_conf,"r")
	for line in msg_file.readlines():
		line = line.strip().rstrip()

		if not len(line) or line.startswith("#"):
			continue
		array_info = line.split("=")
		msgid = array_info[0].strip().rstrip()	#MSGID
		
		array_info = array_info[1].split(',')	#MSGNAME
		msgname = array_info[0].strip().rstrip()
		
		array_info = array_info[1].split("#")		
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

targetMsgIDPath = "../client/Trunk/Assets/Scripts/BaseSystem/Net/Message/Define/MsgIDDefineDic.cs";
targetCSPath = "../client/Trunk/Assets/Scripts/BaseSystem/Net/Message/Define/MsgIDDefine.cs";

f = WrapFile(open(targetMsgIDPath,"w+"))
ParseMsgIDDefineDic(f,l)

f = WrapFile(open(targetCSPath,"w+"))
ParseMsgIDDefine(f,l)

os.system("pause")