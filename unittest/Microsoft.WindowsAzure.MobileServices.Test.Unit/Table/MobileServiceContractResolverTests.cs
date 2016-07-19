﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.MobileServices.Test.Unit.Table
{
    [TestClass]
    public class MobileServiceContractResolverTests
    {
        [TestMethod]
        public void IsThreadSafe()
        {
            const int iterationCount = 100;

            for (int i = 0; i < iterationCount; i++)
            {
                MobileServiceContractResolver contractResolver = new MobileServiceContractResolver();
                Func<JsonContract> resolveContract = () => contractResolver.ResolveContract(typeof(PocoType));
                
                Task t1 = Task.Run(resolveContract);
                Task t2 = Task.Run(resolveContract);                
                Task.WhenAll(t1, t2).Wait();
            }
        }
    }
}
