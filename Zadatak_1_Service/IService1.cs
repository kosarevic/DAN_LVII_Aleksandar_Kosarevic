using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Zadatak_1_Service.Model;

namespace Zadatak_1_Service
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Article> GetData();

        [OperationContract]
        void BuyArticles(List<Article>AllArticles, Article article, int Quantity);
    }
}
