﻿using System;
using System.Collections.Generic;

using System.Text;
using COSXML.Network;
using COSXML.Model;
using COSXML.Model.Service;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.Auth;
using COSXML.Log;
using COSXML.Utils;

namespace COSXML
{
    public class CosXmlServer : CosXml
    {
        private CosXmlConfig config;

        private QCloudCredentialProvider qcloudCredentailProvider;

        private HttpClient httpClient;

        public CosXmlServer(CosXmlConfig config, QCloudCredentialProvider qcloudCredentailProvider)
        {
            if (config != null)
            {
                this.config = config;
            }
            else
            {
                this.config = new CosXmlConfig.Builder().Build();
            }
            if(this.config.IsDebugLog)
            {
                QLog.AddLogAdapter(new LogImpl());
            }
            this.qcloudCredentailProvider = qcloudCredentailProvider;
            HttpClient.Init(this.config.HttpConfig, this.qcloudCredentailProvider);
            httpClient = HttpClient.GetInstance();
        }

        private void CheckAppidAndRegion(CosRequest request)
        {
            if (request.IsHttps == null)
            {
                request.IsHttps = config.IsHttps;
            }
            if (request is GetServiceRequest) return;
            if (request.APPID == null) request.APPID = config.Appid;
            if (request is ObjectRequest)
            {
                if (((ObjectRequest)request).Region == null)
                {
                    ((ObjectRequest)request).Region = config.Region;
                }
                return;
            }
            if (request is BucketRequest)
            {
                if (((BucketRequest)request).Region == null)
                {
                    ((BucketRequest)request).Region = config.Region;
                }
                return;
            }

        }

        private CosResult excute(CosRequest request, CosResult result)
        {
            CheckAppidAndRegion(request);
            httpClient.Excute(request, result);
            return result;
        }

        private void schedue(CosRequest request, CosResult result, COSXML.Callback.OnSuccessCallback<CosResult> successCallback, COSXML.Callback.OnFailedCallback failCallback)
        {
            CheckAppidAndRegion(request);
            httpClient.Schedue(request, result, successCallback, failCallback);
        }

        public Model.Service.GetServiceResult GetService(Model.Service.GetServiceRequest request)
        {
            return (Model.Service.GetServiceResult)excute(request, new Model.Service.GetServiceResult());
        }

        public void GetService(Model.Service.GetServiceRequest request, COSXML.Callback.OnSuccessCallback<CosResult> successCallback, COSXML.Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetServiceResult(), successCallback, failCallback);
        }

        public PutBucketResult PutBucket(PutBucketRequest request)
        {
            return (Model.Bucket.PutBucketResult)excute(request, new Model.Bucket.PutBucketResult());
        }

        public void PutBucket(PutBucketRequest request, COSXML.Callback.OnSuccessCallback<CosResult> successCallback, COSXML.Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketResult(), successCallback, failCallback);
        }

        public DeleteBucketResult DeleteBucket(DeleteBucketRequest request)
        {
            return (Model.Bucket.DeleteBucketResult)excute(request, new Model.Bucket.DeleteBucketResult());
        }

        public void DeleteBucket(DeleteBucketRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteBucketResult(), successCallback, failCallback);
        }

        public HeadBucketResult HeadBucket(HeadBucketRequest request)
        {
            return (Model.Bucket.HeadBucketResult)excute(request, new Model.Bucket.HeadBucketResult());
        }

        public void HeadBucket(HeadBucketRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new HeadBucketResult(), successCallback, failCallback);
        }

        public GetBucketResult GetBucket(GetBucketRequest request)
        {
            return (Model.Bucket.GetBucketResult)excute(request, new Model.Bucket.GetBucketResult());
        }

        public void GetBucket(GetBucketRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketResult(), successCallback, failCallback);
        }

        public GetBucketLocationResult GetBucketLocation(GetBucketLocationRequest request)
        {
            return (Model.Bucket.GetBucketLocationResult)excute(request, new Model.Bucket.GetBucketLocationResult());
        }

        public void GetBucketLocation(GetBucketLocationRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketLocationResult(), successCallback, failCallback);
        }

        public PutBucketACLResult PutBucketACL(PutBucketACLRequest request)
        {
            return (Model.Bucket.PutBucketACLResult)excute(request, new Model.Bucket.PutBucketACLResult());
        }

        public void PutBucketACL(PutBucketACLRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketACLResult(), successCallback, failCallback);
        }

        public GetBucketACLResult GetBucketACL(GetBucketACLRequest request)
        {
            return (Model.Bucket.GetBucketACLResult)excute(request, new Model.Bucket.GetBucketACLResult());
        }

        public void GetBucketACL(GetBucketACLRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketACLResult(), successCallback, failCallback);
        }

        public PutBucketCORSResult PutBucketCORS(PutBucketCORSRequest request)
        {
            return (Model.Bucket.PutBucketCORSResult)excute(request, new Model.Bucket.PutBucketCORSResult());
        }

        public void PutBucketCORS(PutBucketCORSRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketCORSResult(), successCallback, failCallback);
        }

        public GetBucketCORSResult GetBucketCORS(GetBucketCORSRequest request)
        {
            return (Model.Bucket.GetBucketCORSResult)excute(request, new Model.Bucket.GetBucketCORSResult());
        }

        public void GetBucketCORS(GetBucketCORSRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketCORSResult(), successCallback, failCallback);
        }

        public DeleteBucketCORSResult DeleteBucketCORS(DeleteBucketCORSRequest request)
        {
            return (Model.Bucket.DeleteBucketCORSResult)excute(request, new Model.Bucket.DeleteBucketCORSResult());
        }

        public void DeleteBucketCORS(DeleteBucketCORSRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteBucketCORSResult(), successCallback, failCallback);
        }

        public PutBucketLifecycleResult PutBucketLifecycle(PutBucketLifecycleRequest request)
        {
            return (Model.Bucket.PutBucketLifecycleResult)excute(request, new Model.Bucket.PutBucketLifecycleResult());
        }

        public void PutBucketLifecycle(PutBucketLifecycleRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketLifecycleResult(), successCallback, failCallback);
        }

        public GetBucketLifecycleResult GetBucketLifecycle(GetBucketLifecycleRequest request)
        {
            return (Model.Bucket.GetBucketLifecycleResult)excute(request, new Model.Bucket.GetBucketLifecycleResult());
        }

        public void GetBucketLifecycle(GetBucketLifecycleRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketLifecycleResult(), successCallback, failCallback);
        }

        public DeleteBucketLifecycleResult DeleteBucketLifecycle(DeleteBucketLifecycleRequest request)
        {
            return (Model.Bucket.DeleteBucketLifecycleResult)excute(request, new Model.Bucket.DeleteBucketLifecycleResult());
        }

        public void DeleteBucketLifecycle(DeleteBucketLifecycleRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteBucketLifecycleResult(), successCallback, failCallback);
        }

        public PutBucketReplicationResult PutBucketReplication(PutBucketReplicationRequest request)
        {
            return (Model.Bucket.PutBucketReplicationResult)excute(request, new Model.Bucket.PutBucketReplicationResult());
        }

        public void PutBucketReplication(PutBucketReplicationRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketReplicationResult(), successCallback, failCallback);
        }

        public GetBucketReplicationResult GetBucketReplication(GetBucketReplicationRequest request)
        {
            return (Model.Bucket.GetBucketReplicationResult)excute(request, new Model.Bucket.GetBucketReplicationResult());
        }


        public void GetBucketReplication(GetBucketReplicationRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketReplicationResult(), successCallback, failCallback);
        }

        public DeleteBucketReplicationResult DeleteBucketReplication(DeleteBucketReplicationRequest request)
        {
            return (Model.Bucket.DeleteBucketReplicationResult)excute(request, new Model.Bucket.DeleteBucketReplicationResult());
        }

        public void DeleteBucketReplication(DeleteBucketReplicationRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteBucketReplicationResult(), successCallback, failCallback);
        }

        public PutBucketVersioningResult PutBucketVersioning(PutBucketVersioningRequest request)
        {
            return (Model.Bucket.PutBucketVersioningResult)excute(request, new Model.Bucket.PutBucketVersioningResult());
        }

        public void PutBucketVersioning(PutBucketVersioningRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutBucketVersioningResult(), successCallback, failCallback);
        }

        public GetBucketVersioningResult GetBucketVersioning(GetBucketVersioningRequest request)
        {
            return (Model.Bucket.GetBucketVersioningResult)excute(request, new Model.Bucket.GetBucketVersioningResult());
        }

        public void GetBucketVersioning(GetBucketVersioningRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetBucketVersioningResult(), successCallback, failCallback);
        }

        public ListBucketVersionsResult ListBucketVersions(ListBucketVersionsRequest request)
        {
            return (Model.Bucket.ListBucketVersionsResult)excute(request, new Model.Bucket.ListBucketVersionsResult());
        }

        public void ListBucketVersions(ListBucketVersionsRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new ListBucketVersionsResult(), successCallback, failCallback);
        }

        public ListMultiUploadsResult ListMultiUploads(ListMultiUploadsRequest request)
        {
            return (Model.Bucket.ListMultiUploadsResult)excute(request, new Model.Bucket.ListMultiUploadsResult());
        }

        public void ListMultiUploads(ListMultiUploadsRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new ListMultiUploadsResult(), successCallback, failCallback);
        }

        public DeleteBucketPolicyResult DeleteBucketPolicy(DeleteBucketPolicyRequest request)
        {
            return (Model.Bucket.DeleteBucketPolicyResult)excute(request, new Model.Bucket.DeleteBucketPolicyResult());
        }

        public void DeleteBucketPolicy(DeleteBucketPolicyRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteBucketPolicyResult(), successCallback, failCallback);
        }

        public PutObjectResult PutObject(PutObjectRequest request)
        {
            return (Model.Object.PutObjectResult)excute(request, new Model.Object.PutObjectResult());
        }

        public void PutObject(PutObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutObjectResult(), successCallback, failCallback);
        }

        public HeadObjectResult HeadObject(HeadObjectRequest request)
        {
            return (Model.Object.HeadObjectResult)excute(request, new Model.Object.HeadObjectResult());
        }

        public void HeadObject(HeadObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new HeadObjectResult(), successCallback, failCallback);
        }

        public GetObjectResult GetObject(GetObjectRequest request)
        {
            return (Model.Object.GetObjectResult)excute(request, new Model.Object.GetObjectResult());
        }

        public void GetObject(GetObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetObjectResult(), successCallback, failCallback);
        }

        public PutObjectACLResult PutObjectACL(PutObjectACLRequest request)
        {
            return (Model.Object.PutObjectACLResult)excute(request, new Model.Object.PutObjectACLResult());
        }

        public void PutObjectACL(PutObjectACLRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PutObjectACLResult(), successCallback, failCallback);
        }

        public GetObjectACLResult GetObjectACL(GetObjectACLRequest request)
        {
            return (Model.Object.GetObjectACLResult)excute(request, new Model.Object.GetObjectACLResult());
        }

        public void GetObjectACL(GetObjectACLRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new GetObjectACLResult(), successCallback, failCallback);
        }

        public DeleteObjectResult DeleteObject(DeleteObjectRequest request)
        {
            return (Model.Object.DeleteObjectResult)excute(request, new Model.Object.DeleteObjectResult());
        }

        public void DeleteObject(DeleteObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteObjectResult(), successCallback, failCallback);
        }

        public DeleteMultiObjectResult DeleteMultiObjects(DeleteMultiObjectRequest request)
        {
            return (Model.Object.DeleteMultiObjectResult)excute(request, new Model.Object.DeleteMultiObjectResult());
        }

        public void DeleteMultiObjects(DeleteMultiObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new DeleteMultiObjectResult(), successCallback, failCallback);
        }

        public InitMultipartUploadResult InitMultipartUpload(InitMultipartUploadRequest request)
        {
            return (Model.Object.InitMultipartUploadResult)excute(request, new Model.Object.InitMultipartUploadResult());
        }

        public void InitMultipartUpload(InitMultipartUploadRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new InitMultipartUploadResult(), successCallback, failCallback);
        }

        public ListPartsResult ListParts(ListPartsRequest request)
        {
            return (Model.Object.ListPartsResult)excute(request, new Model.Object.ListPartsResult());
        }

        public void ListParts(ListPartsRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new ListPartsResult(), successCallback, failCallback);
        }

        public UploadPartResult UploadPart(UploadPartRequest request)
        {
            return (Model.Object.UploadPartResult)excute(request, new Model.Object.UploadPartResult());
        }

        public void UploadPart(UploadPartRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new UploadPartResult(), successCallback, failCallback);
        }

        public CompleteMultiUploadResult CompleteMultiUpload(CompleteMultiUploadRequest request)
        {
            return (Model.Object.CompleteMultiUploadResult)excute(request, new Model.Object.CompleteMultiUploadResult());
        }

        public void CompleteMultiUpload(CompleteMultiUploadRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new CompleteMultiUploadResult(), successCallback, failCallback);
        }

        public AbortMultiUploadResult AbortMultiUpload(AbortMultiUploadRequest request)
        {
            return (Model.Object.AbortMultiUploadResult)excute(request, new Model.Object.AbortMultiUploadResult());
        }

        public void AbortMultiUpload(AbortMultiUploadRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new AbortMultiUploadResult(), successCallback, failCallback);
        }

        public CopyObjectResult CopyObject(CopyObjectRequest request)
        {
            return (Model.Object.CopyObjectResult)excute(request, new Model.Object.CopyObjectResult());
        }

        public void CopyObject(CopyObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new CopyObjectResult(), successCallback, failCallback);
        }

        public UploadPartCopyResult PartCopy(UploadPartCopyRequest request)
        {
            return (Model.Object.UploadPartCopyResult)excute(request, new Model.Object.UploadPartCopyResult());
        }

        public void PartCopy(UploadPartCopyRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new UploadPartCopyResult(), successCallback, failCallback);
        }

        public OptionObjectResult OptionObject(OptionObjectRequest request)
        {
            return (Model.Object.OptionObjectResult)excute(request, new Model.Object.OptionObjectResult());
        }

        public void OptionObject(OptionObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new OptionObjectResult(), successCallback, failCallback);
        }

        public PostObjectResult PostObject(PostObjectRequest request)
        {
            return (Model.Object.PostObjectResult)excute(request, new Model.Object.PostObjectResult());
        }

        public void PostObject(PostObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new PostObjectResult(), successCallback, failCallback);
        }

        public RestoreObjectResult RestoreObject(RestoreObjectRequest request)
        {
            return (Model.Object.RestoreObjectResult)excute(request, new Model.Object.RestoreObjectResult());
        }

        public void RestoreObject(RestoreObjectRequest request, Callback.OnSuccessCallback<CosResult> successCallback, Callback.OnFailedCallback failCallback)
        {
            schedue(request, new RestoreObjectResult(), successCallback, failCallback);
        }

        public string GenerateSign(string method, string path, Dictionary<string, string> queryParameters, Dictionary<string, string> headers, string signTime)
        {
            return CosXmlSigner.GenerateSign(method, path, queryParameters, headers,signTime, qcloudCredentailProvider.GetQCloudCredentials());
        }

        public string GenerateSignURL(CosRequest request, Dictionary<string, string> queryParameters, Dictionary<string, string> headers, string signTime)
        {
            string url = GetAccessURL(request);
            string sign = GenerateSign(request.Method, request.RequestPath, queryParameters, headers, signTime);
            return url + "?sign=" + URLEncodeUtils.Encode(sign);
        }

        public string GetAccessURL(CosRequest request)
        {
            request.CheckParameters();
            StringBuilder urlBuilder = new StringBuilder();
            if (request.IsHttps != null && (bool)request.IsHttps)
            {
                urlBuilder.Append("https://");
            }
            else
            {
                urlBuilder.Append("http://");
            }
            urlBuilder.Append(request.GetHost());
            urlBuilder.Append(request.RequestPath);
            return urlBuilder.ToString();
        }
    }
}