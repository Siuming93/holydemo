import sys
import os
import shutil

msg_dir = "./thrift/"
msgid_conf = "./msg/msgid.conf"
msg_namespace = "Monster.Protocol"


def compile_proto(proto_dir,target_dir):
	files = os.listdir(proto_dir)
	for f in files:
		if not f.endswith('.thrift'):
			continue
		os.system(".\\thrift\\thrift.exe " + "--gen csharp " + proto_dir + f)



def  move_script(script_dir, target_dir):
	files = os.listdir(script_dir)

	for f in files:
		if os.path.isdir(script_dir + f+"/"):
			move_script(script_dir + f+"/", target_dir)
			continue
		if f.endswith(".cs"):
			shutil.move(script_dir + f, target_dir + f)
		
def get_targetDir():
	curPath = os.getcwd()
	return curPath.replace("tech","client/Trunk/Assets/Scripts/BaseSystem/Net/Message/Protocol/")

target_dir = get_targetDir()
compile_proto(msg_dir, target_dir)
move_script(msg_dir, target_dir)