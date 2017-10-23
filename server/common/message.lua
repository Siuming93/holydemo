local message = {}

message.CSLOGIN                                                      	= 10001; 
message.SCLOGIN                                                      	= 10002; 
message.CSHELLOWORLD                                                 	= 10201; 
message.SCHELLOWORLD                                                 	= 10202; 
message.CSTALK                                                       	= 10301; 
message.SCTALK                                                       	= 10302; 
message.CSENTERSCENE                                                 	= 10401; 
message.SCENTERSCENE                                                 	= 10402; 
message.CSPLAYERMOVE                                                 	= 10403; 
message.SCALLPLAYERPOSINFO                                           	= 10404; //所有玩家位置状态;定期发
message.CSPLAYERISMOVE                                               	= 10405; 
message.SCPLAYERISMOVE                                               	= 10406; 
message.CSPLAYERUSESKILL                                             	= 10407; 
message.SCPLAYERUSESKILL                                             	= 10408; 

return message
