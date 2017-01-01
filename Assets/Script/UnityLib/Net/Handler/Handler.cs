using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityLib.Framework.Net {
    public class Handler<T>  {
        public class HandlerContext {
            public HttpWebRequest Request { get; set; }
            public HttpWebResponse Response { get; set; }
            public StreamReader Reader { get; set; }
            public Stream IOStream { get; set; }

            public byte[] Payload { get; set; }
            public int PayloadSize { get; set; }

            public string json { get; set; }

            public HandlerContext() {
                Payload = new byte[128];
            }
        }

        public System.Func<T> onDone { get; private set; }
        private string jsonPayload;

        public Handler() {
            
        }

        public Handler(string payload) {
            jsonPayload = payload;
        }

        public void Reset() {
            onDone = null;
        }

        public void SendGetRequest(string method, string resource) {
            try {
                HandlerContext context = new HandlerContext();
                context.Request = (HttpWebRequest)WebRequest.Create(resource);
                context.Request.Method = method;

                context.Request.BeginGetResponse(
                    new AsyncCallback(OnResponse),
                    context
                );
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }

        public void SendJsonRequest(string method, string resource) {
            try {
                Debug.LogFormat("registered callback: {0}", Time.time);

                HandlerContext context = new HandlerContext();

                context.Payload = Encoding.UTF8.GetBytes(jsonPayload);
                context.PayloadSize = jsonPayload.Length;

                context.Request = (HttpWebRequest)WebRequest.Create(resource);
                context.Request.Method = method;
                context.Request.ContentType = "application/json; charset=utf-8";
                context.Request.ContentLength = context.PayloadSize;

                context.Request.BeginGetRequestStream(
                    new AsyncCallback(OnRequest),
                    context
                );
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }

        public void OnRequest(IAsyncResult ar) {
            try {
                HandlerContext context = (HandlerContext) ar.AsyncState;

                context.IOStream = context.Request.EndGetRequestStream(ar);
                context.IOStream.Write(context.Payload, 0, context.PayloadSize);
                context.IOStream.Close();

                context.Request.BeginGetResponse(
                    new AsyncCallback(OnResponse),
                    context
                );
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }

        }

        public void OnResponse(IAsyncResult ar) {
            try {
                HandlerContext context = (HandlerContext) ar.AsyncState;

                context.Response = (HttpWebResponse) context.Request.EndGetResponse(ar);
                context.IOStream = context.Response.GetResponseStream();
                context.Reader = new StreamReader(context.IOStream);
                context.json = context.Reader.ReadToEnd();

                Debug.LogFormat("server sent (json): {0} ", context.json);

                onDone = () => {
                    Debug.LogFormat("callback called: {0}", Time.time);
                    return JsonUtility.FromJson<T>(context.json);
                };

                context.Reader.Close();
                context.Response.Close();
                context.IOStream.Close();
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }
    }
}
