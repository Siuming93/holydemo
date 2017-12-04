local message = {}

message.CSLOGIN                                                      	= 10001; 
message.SCLOGIN                                                      	= 10002; 
message.CSASYNCTIME                                                  	= 10003; 
message.SCASYNCTIME                                                  	= 10004; 
message.CSHELLOWORLD                                                 	= 10201; 
message.SCHELLOWORLD                                                 	= 10202; 
message.CSTALK                                                       	= 10301; 
message.SCTALK                                                       	= 10302; 
message.CSENTERSCENE                                                 	= 10401; 
message.SCENTERSCENE                                                 	= 10402; 
message.SCALLPLAYERPOSINFO                                           	= 10404; --所有玩家位置状态;定期发
message.CSPLAYERSTARTMOVE                                            	= 10405; 
message.SCPLAYERSTARTMOVE                                            	= 10406; 
message.CSPLAYERENDMOVE                                              	= 10407; 
message.SCPLAYERENDMOVE                                              	= 10408; 
message.CSPLAYERUSESKILL                                             	= 10409; 
message.SCPLAYERUSESKILL                                             	= 10410; 
message.CSPLAYERUPDATEMOVEDIR                                        	= 10411; 
message.SCPLAYERUPDATEMOVEDIR                                        	= 10412; 
message.SCOTHERROLEENTERSCENE                                        	= 10413; 
message.CSLEAVESCENE                                                 	= 10490; 
message.SCLEAVESCENE                                                 	= 10491; 

return message
