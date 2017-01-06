using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityLib.Net {
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
        private string endpoint;

        public Handler() {}

        public Handler(string payload) {
            jsonPayload = payload;
        }

        public void Reset() {
            onDone = null;
        }

        public void GET(string resource) {
            try {
                endpoint = resource;

                Debug.LogFormat("[+] GET {0} [{1}]", endpoint, Time.time);

                HandlerContext context = new HandlerContext();
        
                context.Request = (HttpWebRequest)WebRequest.Create(endpoint);
                context.Request.Method = "GET";

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

        public void POST(string resource) {
            try {
                endpoint = resource;

                Debug.LogFormat("[+] POST {0} [{1}]", endpoint, Time.time);

                HandlerContext context = new HandlerContext();

                context.Payload = Encoding.UTF8.GetBytes(jsonPayload);
                context.PayloadSize = jsonPayload.Length;
                context.Request = (HttpWebRequest)WebRequest.Create(endpoint);
                context.Request.Method = "POST";
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

        private void OnRequest(IAsyncResult ar) {
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

        private void OnResponse(IAsyncResult ar) {
            try {
                HandlerContext context = (HandlerContext) ar.AsyncState;

                context.Response = (HttpWebResponse) context.Request.EndGetResponse(ar);
                context.IOStream = context.Response.GetResponseStream();
                context.Reader = new StreamReader(context.IOStream);
                context.json = context.Reader.ReadToEnd();

                Debug.LogFormat("[+] resource @ [{0}]", endpoint);
                Debug.LogFormat(" - [+] yielded (json): {0} ", context.json);

                onDone = () => {
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
