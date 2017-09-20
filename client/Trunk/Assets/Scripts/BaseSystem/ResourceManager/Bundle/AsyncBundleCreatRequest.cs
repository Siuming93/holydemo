using System.Collections.Generic;
using UnityEngine;

namespace Monster.BaseSystem.ResourceManager
{
     class AsyncBundleCreatRequest: IAsyncRequest
     {
         public string id { get; set; }

         public bool isDone
         {
             get
             {
                 for (int i = 0,count = dependenceRequestList.Count; i<count ; i++)
                 {
                     if (!dependenceRequestList[i].isDone)
                         return false;
                 }

                 return createRequest.isDone;
             }
         }

         public AssetBundleCreateRequest createRequest;
         public AssetBundleHint hint;
         public List<AsyncBundleCreatRequest> dependenceRequestList = new List<AsyncBundleCreatRequest>();

     }
}
