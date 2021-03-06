﻿using COSXML.Common;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Bucket;
using COSXML.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace COSXMLTests
{ 

    [TestFixture()]
    public class BucketTest
    {

        public void PutBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketRequest request = new PutBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                // 添加acl
                request.SetCosACL(CosACL.PUBLIC_READ);

                COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
                readAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantRead(readAccount);

                COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
                writeAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantWrite(writeAccount);

                COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
                fullAccount.AddGrantAccount("2832742109", "2832742109");
                request.SetXCosReadWrite(fullAccount);

                //执行请求
                PutBucketResult result = cosXml.PutBucket(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                if (serverEx.statusCode != 409)
                {
                    Assert.Fail();
                }
            }
        }

        public void AsyncPutBucket(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketRequest request = new PutBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            // 添加acl
            request.SetCosACL("public-read");

            COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
            readAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantRead(readAccount);

            COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
            writeAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantWrite(writeAccount);

            COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
            fullAccount.AddGrantAccount("2832742109", "2832742109");
            request.SetXCosReadWrite(fullAccount);

            //执行请求
            cosXml.PutBucket(request,
                delegate (CosResult cosResult)
            {
                PutBucketResult result = cosResult as PutBucketResult;
                Console.WriteLine(result.GetResultInfo());

                manualResetEvent.Set();
            },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                    Assert.Fail();
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    if (serverEx.statusCode != 409)
                    {
                        Assert.Fail();
                    }
                }

                manualResetEvent.Set();
            });
            manualResetEvent.WaitOne();
        }



        public void HeadBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                HeadBucketRequest request = new HeadBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                HeadBucketResult result = cosXml.HeadBucket(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncHeadBucket(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            HeadBucketRequest request = new HeadBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.HeadBucket(request,
                delegate (CosResult cosResult)
                {
                    HeadBucketResult result = cosResult as HeadBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();

        }


        public void GetBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketRequest request = new GetBucketRequest(bucket);

                //设置签名有效时长
                //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                request.SetPrefix("a/中文/d");

                List<string> headerKeys = new List<string>();
                headerKeys.Add("Host");


                List<string> queryParameters = new List<string>();
                queryParameters.Add("prefix");
                queryParameters.Add("max-keys");

                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, headerKeys, queryParameters);


                //执行请求
                GetBucketResult result = cosXml.GetBucket(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncGetBucket(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketRequest request = new GetBucketRequest(bucket);
            request.SetPrefix("a/bed/d");
            List<string> queryParameters = new List<string>();
            queryParameters.Add("prefix");
            queryParameters.Add("max-keys");
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600, null, queryParameters);
            ////设置签名有效时长
            //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            ///执行请求
            cosXml.GetBucket(request,
                delegate (CosResult cosResult)
                {
                    GetBucketResult result = cosResult as GetBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();

        }


        public void GetBuckeLocation(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketLocationRequest request = new GetBucketLocationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketLocationResult result = cosXml.GetBucketLocation(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncGetBuckeLocation(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketLocationRequest request = new GetBucketLocationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketLocation(request,
                delegate (CosResult cosResult)
                {
                    GetBucketLocationResult result = cosResult as GetBucketLocationResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void PutBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketACLRequest request = new PutBucketACLRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //添加acl
                request.SetCosACL(CosACL.PUBLIC_READ);

                COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
                readAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantRead(readAccount);

                COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
                writeAccount.AddGrantAccount("1131975903", "1131975903");
                request.SetXCosGrantWrite(writeAccount);

                COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
                fullAccount.AddGrantAccount("2832742109", "2832742109");
                request.SetXCosReadWrite(fullAccount);

                //执行请求
                PutBucketACLResult result = cosXml.PutBucketACL(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        public  void AsyncPutBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketACLRequest request = new PutBucketACLRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //添加acl
            request.SetCosACL("private");

            COSXML.Model.Tag.GrantAccount readAccount = new COSXML.Model.Tag.GrantAccount();
            readAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantRead(readAccount);

            COSXML.Model.Tag.GrantAccount writeAccount = new COSXML.Model.Tag.GrantAccount();
            writeAccount.AddGrantAccount("1131975903", "1131975903");
            request.SetXCosGrantWrite(writeAccount);

            COSXML.Model.Tag.GrantAccount fullAccount = new COSXML.Model.Tag.GrantAccount();
            fullAccount.AddGrantAccount("2832742109", "2832742109");
            request.SetXCosReadWrite(fullAccount);

            ///执行请求
            cosXml.PutBucketACL(request,
                delegate (CosResult cosResult)
                {
                    PutBucketACLResult result = cosResult as PutBucketACLResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();
        }

        public void GetBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketACLRequest request = new GetBucketACLRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketACLResult result = cosXml.GetBucketACL(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.StackTrace);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
        }

        public  void AsyncGetBucketACL(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketACLRequest request = new GetBucketACLRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketACL(request,
                delegate (CosResult cosResult)
                {
                    GetBucketACLResult result = cosResult as GetBucketACLResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();
        }


        public void PutBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketCORSRequest request = new PutBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置cors
                COSXML.Model.Tag.CORSConfiguration.CORSRule corsRule = new COSXML.Model.Tag.CORSConfiguration.CORSRule();

                corsRule.id = "corsconfigure1";
                corsRule.maxAgeSeconds = 6000;
                corsRule.allowedOrigin = "http://cloud.tencent.com";

                corsRule.allowedMethods = new List<string>();
                corsRule.allowedMethods.Add("PUT");
                corsRule.allowedMethods.Add("DELETE");
                corsRule.allowedMethods.Add("POST");

                corsRule.allowedHeaders = new List<string>();
                corsRule.allowedHeaders.Add("Host");
                corsRule.allowedHeaders.Add("Authorizaiton");
                corsRule.allowedHeaders.Add("User-Agent");

                corsRule.exposeHeaders = new List<string>();
                corsRule.exposeHeaders.Add("x-cos-meta-x1");
                corsRule.exposeHeaders.Add("x-cos-meta-x2");

                request.SetCORSRule(corsRule);

                //执行请求
                PutBucketCORSResult result = cosXml.PutBucketCORS(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncPutBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketCORSRequest request = new PutBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置cors
            COSXML.Model.Tag.CORSConfiguration.CORSRule corsRule = new COSXML.Model.Tag.CORSConfiguration.CORSRule();

            corsRule.id = "corsconfigure1";
            corsRule.maxAgeSeconds = 6000;
            corsRule.allowedOrigin = "http://cloud.tencent.com";

            corsRule.allowedMethods = new List<string>();
            corsRule.allowedMethods.Add("PUT");
            corsRule.allowedMethods.Add("DELETE");
            corsRule.allowedMethods.Add("POST");

            corsRule.allowedHeaders = new List<string>();
            corsRule.allowedHeaders.Add("Host");
            corsRule.allowedHeaders.Add("Authorizaiton");
            corsRule.allowedHeaders.Add("User-Agent");

            corsRule.exposeHeaders = new List<string>();
            corsRule.exposeHeaders.Add("x-cos-meta-x1");
            corsRule.exposeHeaders.Add("x-cos-meta-x2");

            List<COSXML.Model.Tag.CORSConfiguration.CORSRule> cORSRules = new List<COSXML.Model.Tag.CORSConfiguration.CORSRule>();
            cORSRules.Add(corsRule);
            request.SetCORSRules(cORSRules);

            ///执行请求
            cosXml.PutBucketCORS(request,
                delegate (CosResult cosResult)
                {
                    PutBucketCORSResult result = cosResult as PutBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();
        }


        public void GetBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketCORSRequest request = new GetBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketCORSResult result = cosXml.GetBucketCORS(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }
            
        }

        public  void AsyncGetBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketCORSRequest request = new GetBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketCORS(request,
                delegate (CosResult cosResult)
                {
                    GetBucketCORSResult result = cosResult as GetBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();
        }
        public void DeleteBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketCORSRequest request = new DeleteBucketCORSRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketCORSResult result = cosXml.DeleteBucketCORS(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncDeleteBucketCORS(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            DeleteBucketCORSRequest request = new DeleteBucketCORSRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucketCORS(request,
                delegate (CosResult cosResult)
                {
                    DeleteBucketCORSResult result = cosResult as DeleteBucketCORSResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }

        public void PutBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketLifecycleRequest request = new PutBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置 lifecycle
                COSXML.Model.Tag.LifecycleConfiguration.Rule rule = new COSXML.Model.Tag.LifecycleConfiguration.Rule();
                rule.id = "lfiecycleConfigure2";
                rule.status = "Enabled"; //Enabled，Disabled

                rule.filter = new COSXML.Model.Tag.LifecycleConfiguration.Filter();
                rule.filter.prefix = "2/";

                rule.abortIncompleteMultiUpload = new COSXML.Model.Tag.LifecycleConfiguration.AbortIncompleteMultiUpload();
                rule.abortIncompleteMultiUpload.daysAfterInitiation = 2;

                request.SetRule(rule);

                //执行请求
                PutBucketLifecycleResult result = cosXml.PutBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncPutBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketLifecycleRequest request = new PutBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //设置 lifecycle
            COSXML.Model.Tag.LifecycleConfiguration.Rule rule = new COSXML.Model.Tag.LifecycleConfiguration.Rule();
            rule.id = "lfiecycleConfig3";
            rule.status = "Enabled"; //Enabled，Disabled

            rule.filter = new COSXML.Model.Tag.LifecycleConfiguration.Filter();
            rule.filter.prefix = "3";

            rule.abortIncompleteMultiUpload = new COSXML.Model.Tag.LifecycleConfiguration.AbortIncompleteMultiUpload();
            rule.abortIncompleteMultiUpload.daysAfterInitiation = 2;

            List<COSXML.Model.Tag.LifecycleConfiguration.Rule> rules = new List<COSXML.Model.Tag.LifecycleConfiguration.Rule>();
            rules.Add(rule);

            request.SetRules(rules);

            ///执行请求
            cosXml.PutBucketLifecycle(request,
                delegate (CosResult cosResult)
                {
                    PutBucketLifecycleResult result = cosResult as PutBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void GetBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketLifecycleRequest request = new GetBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketLifecycleResult result = cosXml.GetBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncGetBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketLifecycleRequest request = new GetBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.GetBucketLifecycle(request,
                delegate (CosResult cosResult)
                {
                    GetBucketLifecycleResult result = cosResult as GetBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void DeleteBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketLifecycleRequest request = new DeleteBucketLifecycleRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketLifecycleResult result = cosXml.DeleteBucketLifecycle(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncDeleteBucketLifeCycle(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            DeleteBucketLifecycleRequest request = new DeleteBucketLifecycleRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucketLifecycle(request,
                delegate (CosResult cosResult)
                {
                    DeleteBucketLifecycleResult result = cosResult as DeleteBucketLifecycleResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void PutBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketReplicationRequest request = new PutBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //设置replication
                PutBucketReplicationRequest.RuleStruct ruleStruct = new PutBucketReplicationRequest.RuleStruct();
                ruleStruct.appid = "";
                ruleStruct.bucket = "";
                ruleStruct.region = "";
                ruleStruct.isEnable = true;
                ruleStruct.storageClass = EnumUtils.GetValue(CosStorageClass.STANDARD);
                ruleStruct.id = "";
                ruleStruct.prefix = "";
                List<PutBucketReplicationRequest.RuleStruct> ruleStructs = new List<PutBucketReplicationRequest.RuleStruct>();

                request.SetReplicationConfiguration("2832742109", "2832742109", ruleStructs);

                //执行请求
                PutBucketReplicationResult result = cosXml.PutBucketReplication(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                //Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                //Assert.Fail();
            }

        }

        public  void AsyncPutBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketReplicationRequest request = new PutBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
            //设置replication
      
            PutBucketReplicationRequest.RuleStruct ruleStruct = new PutBucketReplicationRequest.RuleStruct();
            ruleStruct.appid = "";
            ruleStruct.bucket = "";
            ruleStruct.region = "";
            ruleStruct.isEnable = true;
            ruleStruct.storageClass = "";
            ruleStruct.id = "";
            ruleStruct.prefix = "";
            List<PutBucketReplicationRequest.RuleStruct> ruleStructs = new List<PutBucketReplicationRequest.RuleStruct>();

            request.SetReplicationConfiguration("2832742109", "2832742109", ruleStructs);
            ///执行请求
            cosXml.PutBucketReplication(request,
                delegate (CosResult cosResult)
                {
                    PutBucketReplicationResult result = cosResult as PutBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
               // Assert.Fail();
                manualResetEvent.Set();
            });
        }

        public void GetBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketReplicationRequest request = new GetBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketReplicationResult result = cosXml.GetBucketReplication(request);


                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                //Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
               // Assert.Fail();
            }

        }

        public  void AsyncGetBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketReplicationRequest request = new GetBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            ///执行请求
            cosXml.GetBucketReplication(request,
                delegate (CosResult cosResult)
                {
                    GetBucketReplicationResult result = cosResult as GetBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                //Assert.Fail();
                manualResetEvent.Set();
            });
        }

        public void DeleteBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketReplicationRequest request = new DeleteBucketReplicationRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketReplicationResult result = cosXml.DeleteBucketReplication(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncDeleteBucketReplication(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            DeleteBucketReplicationRequest request = new DeleteBucketReplicationRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);


            ///执行请求
            cosXml.DeleteBucketReplication(request,
                delegate (CosResult cosResult)
                {
                    DeleteBucketReplicationResult result = cosResult as DeleteBucketReplicationResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void PutBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                PutBucketVersioningRequest request = new PutBucketVersioningRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

                //开启版本控制
                request.IsEnableVersionConfig(true);

                //执行请求
                PutBucketVersioningResult result = cosXml.PutBucketVersioning(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncPutBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            PutBucketVersioningRequest request = new PutBucketVersioningRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //开启版本控制
            request.IsEnableVersionConfig(true);


            ///执行请求
            cosXml.PutBucketVersioning(request,
                delegate (CosResult cosResult)
                {
                    PutBucketVersioningResult result = cosResult as PutBucketVersioningResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }

        public void GetBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                GetBucketVersioningRequest request = new GetBucketVersioningRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketVersioningResult result = cosXml.GetBucketVersioning(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncGetBucketVersioning(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            GetBucketVersioningRequest request = new GetBucketVersioningRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.GetBucketVersioning(request,
                delegate (CosResult cosResult)
                {
                    GetBucketVersioningResult result = cosResult as GetBucketVersioningResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void ListBucketVersions(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                ListBucketVersionsResult result = cosXml.ListBucketVersions(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncListBucketVersions(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.ListBucketVersions(request,
                delegate (CosResult cosResult)
                {
                    ListBucketVersionsResult result = cosResult as ListBucketVersionsResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }


        public void ListMultiUploads(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                ListMultiUploadsResult result = cosXml.ListMultiUploads(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncListMultiUploads(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.ListMultiUploads(request,
                delegate (CosResult cosResult)
                {
                    ListMultiUploadsResult result = cosResult as ListMultiUploadsResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
        }

        public void DeleteBucketPolicy(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketPolicyRequest request = new DeleteBucketPolicyRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketPolicyResult result = cosXml.DeleteBucketPolicy(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public void AsynDeleteBucketPolicy(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            DeleteBucketPolicyRequest request = new DeleteBucketPolicyRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            //执行请求
            cosXml.DeleteBucketPolicy(request,
                delegate (CosResult cosResult)
                {
                    DeleteBucketPolicyResult result = cosResult as DeleteBucketPolicyResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
                delegate (CosClientException clientEx, CosServerException serverEx)
                {
                    if (clientEx != null)
                    {
                        Console.WriteLine("CosClientException: " + clientEx.Message);
                    }
                    if (serverEx != null)
                    {
                        Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                    }
                    Assert.Fail();
                    manualResetEvent.Set();
                });
        }

        public void DeleteBucket(COSXML.CosXml cosXml, string bucket)
        {
            try
            {
                DeleteBucketRequest request = new DeleteBucketRequest(bucket);
                //设置签名有效时长
                //request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteBucketResult result = cosXml.DeleteBucket(request);

                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                Console.WriteLine("CosClientException: " + clientEx.Message);
                Assert.Fail();
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Assert.Fail();
            }

        }

        public  void AsyncDeleteBucket(COSXML.CosXml cosXml, string bucket)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            DeleteBucketRequest request = new DeleteBucketRequest(bucket);
            //设置签名有效时长
            request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);

            ///执行请求
            cosXml.DeleteBucket(request,
                delegate (CosResult cosResult)
                {
                    DeleteBucketResult result = cosResult as DeleteBucketResult;
                    Console.WriteLine(result.GetResultInfo());
                    manualResetEvent.Set();
                },
            delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx.Message);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
                Assert.Fail();
                manualResetEvent.Set();
            });
            manualResetEvent.WaitOne();

        }


        [Test()]
        public void testBucket()
        {
            QCloudServer instance = QCloudServer.Instance();

            PutBucket(instance.cosXml, instance.bucketForBucketTest);

            HeadBucket(instance.cosXml, instance.bucketForBucketTest);

            GetBuckeLocation(instance.cosXml, instance.bucketForBucketTest);

            GetBucket(instance.cosXml, instance.bucketForBucketTest);

            PutBucketACL(instance.cosXml, instance.bucketForBucketTest);
            GetBucketACL(instance.cosXml, instance.bucketForBucketTest);

            DeleteBucketCORS(instance.cosXml, instance.bucketForBucketTest);
            Thread.Sleep(300);
            PutBucketCORS(instance.cosXml, instance.bucketForBucketTest);
            Thread.Sleep(300);
            GetBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            PutBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);
            Thread.Sleep(1000);
            GetBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);
            DeleteBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            PutBucketReplication(instance.cosXml, instance.bucketForBucketTest);
            GetBucketReplication(instance.cosXml, instance.bucketForBucketTest);
            DeleteBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            PutBucketVersioning(instance.cosXml, instance.bucketForBucketTest);
            GetBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            ListBucketVersions(instance.cosXml, instance.bucketForBucketTest);

            ListMultiUploads(instance.cosXml, instance.bucketForBucketTest);

            DeleteBucketPolicy(instance.cosXml, instance.bucketForBucketTest);

            // DeleteBucket(instance.cosXml, instance.bucketForBucketTest);

            Assert.True(true);




            //AsyncPutBucket(instance.cosXml, instance.bucketForBucketTest);

            //AsyncHeadBucket(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBuckeLocation(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucket(instance.cosXml, instance.bucketForBucketTest);

            //AsyncPutBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucketACL(instance.cosXml, instance.bucketForBucketTest);

            //AsyncPutBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //AsyncDeleteBucketCORS(instance.cosXml, instance.bucketForBucketTest);

            //AsyncPutBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //AsyncDeleteBucketLifeCycle(instance.cosXml, instance.bucketForBucketTest);

            //AsyncPutBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //AsyncDeleteBucketReplication(instance.cosXml, instance.bucketForBucketTest);

            //AsyncPutBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //AsyncGetBucketVersioning(instance.cosXml, instance.bucketForBucketTest);

            //AsyncListBucketVersions(instance.cosXml, instance.bucketForBucketTest);

            //AsyncListMultiUploads(instance.cosXml, instance.bucketForBucketTest);

            //AsynDeleteBucketPolicy(instance.cosXml, instance.bucketForBucketTest);

            //AsyncDeleteBucket(instance.cosXml, instance.bucketForBucketTest);


        }

       

    }
}
