local message = {}

message.CSLOGIN                                                      	= 10001; 
message.SCLOGIN                                                      	= 10002; 
message.CSASYNCTIME                                                  	= 10003; 
message.CSASYNCTIME                                                  	= 10004; 
message.CSHELLOWORLD                                                 	= 10201; 
message.SCHELLOWORLD                                                 	= 10202; 
message.CSTALK                                                       	= 10301; 
message.SCTALK                                                       	= 10302; 
message.CSENTERSCENE                                                 	= 10401; 
message.SCENTERSCENE                                                 	= 10402; 
message.CSPLAYERMOVE                                                 	= 10403; 
message.SCALLPLAYERPOSINFO                                           	= 10404; --所有玩家位置状态;定期发
message.CSPLAYERSTARTMOVE                                            	= 10405; 
message.CSPLAYERENDMOVE                                              	= 10406; 
message.SCPLAYERENDMOVE                                              	= 10407; 
message.CSPLAYERENDMOVEPOS                                           	= 10408; 
message.CSPLAYERUSESKILL                                             	= 10409; 
message.SCPLAYERUSESKILL                                             	= 10410; 
message.CSPLAYERUPDATEMOVEDIR                                        	= 10411; 
message.SCPLAYERUPDATEMOVEDIR                                        	= 10412; 
message.SCPLAYERSTARTMOVE                                            	= 10413; 
message.CSASYNCPLAYERPOS                                             	= 10414; 
message.SCASYNCPLAYERPOS                                             	= 10415; 

return message
