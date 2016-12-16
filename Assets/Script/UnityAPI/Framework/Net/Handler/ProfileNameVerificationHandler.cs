using UnityEngine;
using UnityAPI.Game;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityAPI.Framework.Net {
    public class ProfileNameVerificationHandler { // ADD GENERIC TYPE PARAM??
        public class HandlerContext : IHandlerContext {
            public HttpWebRequest Request { get; set; }
            public HttpWebResponse Response { get; set; }

            public byte[] Payload { get; set; }
            public int PayloadSize { get; set; }

            public HandlerContext() {
                Payload = new byte[128];
            }
        }

        public System.Func<ProfileSearch> onDone { get; private set; }

        public void VerifyNameIsAvailable(string json, System.Func<ProfileSearch> cb) {
            try {
                HandlerContext state = new HandlerContext();

                state.Payload = Encoding.UTF8.GetBytes(json);
                state.PayloadSize = json.Length;

                state.Request = (HttpWebRequest) WebRequest.Create(ServiceController.Debug_Addr_Availability);
                state.Request.Method = "POST";
                state.Request.ContentType = "application/json; charset=utf-8";
                state.Request.ContentLength = state.PayloadSize;

                state.Request.BeginGetRequestStream(
                    new AsyncCallback(OnRequest),
                    state
                );
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }

        public void Reset() {
            onDone = null;
        }

        private void OnRequest(IAsyncResult ar) {
            try {
                HandlerContext state = (HandlerContext) ar.AsyncState;

                Stream stream = state.Request.EndGetRequestStream(ar);
                stream.Write(state.Payload, 0, state.PayloadSize);
                stream.Close();

                state.Request.BeginGetResponse(
                    new AsyncCallback(OnResponse),
                    state
                );
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }

        private void OnResponse(IAsyncResult ar) {
            try {
                HandlerContext state = (HandlerContext) ar.AsyncState;
                state.Response = (HttpWebResponse) state.Request.EndGetResponse(ar);
                Stream stream = state.Response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();


                onDone = () => {
                    return JsonUtility.FromJson<ProfileSearch>(response);
                };

                reader.Close();
                stream.Close();

                state.Response.Close();
            } catch (WebException we) {
                Debug.LogError(NetUtil.PrintfWebException(we));
            } catch (Exception e) {
                Debug.LogError(NetUtil.PrintfException(e));
            }
        }
    }
}