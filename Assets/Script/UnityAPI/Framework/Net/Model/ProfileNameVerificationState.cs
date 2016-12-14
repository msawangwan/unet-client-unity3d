using UnityEngine;
using UnityAPI.Model;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityAPI.Framework.Net {
    public class ProfileNameVerificationHandler {
        public class HandlerContext : IHandlerContext {
            public HttpWebRequest Request { get; set; }
            public HttpWebResponse Response { get; set; }

            public byte[] Payload { get; set; }
            public int PayloadSize { get; set; }

            public HandlerContext() {
                Payload = new byte[128];
            }

            public System.Func<ProfileSearch> onVerify;
        }

        public System.Func<ProfileSearch> onDone;

        public void POSTasync(string json, System.Func<ProfileSearch> cb) {
            try {
                HandlerContext state = new HandlerContext();
                state.onVerify = cb;

                state.Request = (HttpWebRequest) WebRequest.Create(ClientServiceHandler.debug_route);
                state.Request.ContentType = "application/json; charset=utf-8";
                state.Request.Method = "POST";

                state.Payload = Encoding.UTF8.GetBytes(json);
                state.PayloadSize = json.Length;

                Debug.LogFormat("making async request, payload size: {0}", state.PayloadSize);

                state.Request.BeginGetRequestStream(
                    new AsyncCallback(POSTrequestStreamCallback),
                    state
                );
            } catch (WebException we) {
                Debug.Log(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.Log(NetUtil.PrintfException(e));
            }
        }

        private void POSTrequestStreamCallback(IAsyncResult ar) {
            try {
                HandlerContext state = (HandlerContext) ar.AsyncState;

                Stream stream = state.Request.EndGetRequestStream(ar);
                stream.Write(state.Payload, 0, state.PayloadSize);
                stream.Close();

                state.Request.BeginGetResponse(
                    new AsyncCallback(POSTresponseCallback),
                    state
                );
            } catch (WebException we) {
                Debug.Log(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.Log(NetUtil.PrintfException(e));
            }
        }

        private void POSTresponseCallback(IAsyncResult ar) {
            try {
                HandlerContext state = (HandlerContext) ar.AsyncState;
                state.Response = (HttpWebResponse) state.Request.EndGetResponse(ar);
                Stream stream = state.Response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();

                Debug.Log(response);
                ProfileSearch p = JsonUtility.FromJson<ProfileSearch>(response);
                onDone = () => { 
                    return p;
                };

                reader.Close();
                stream.Close();

                state.Response.Close();
            } catch (WebException we) {
                Debug.Log(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.Log(NetUtil.PrintfException(e));
            }
        }
    }
}
