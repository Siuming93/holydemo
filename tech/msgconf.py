# _*_ coding:utf-8 _*_
msgid_conf = "./msg/msgid.conf"

def parse_msgfile(msgid_conf):
	msg_info_list = []
	msg_file = open(msgid_conf,"r")
	for line in msg_file:
		print(line)

		
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

f = WrapFile(open(targetPath,'rb',encoding='UTF-8'))
ParseMsgIDDefine(f,l)