using UnityEngine;
using UnityAPI.Game;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityAPI.Framework.Net {
    public class ProfileCreateHandler {
        public class HandlerContext : IHandlerContext {
            public HttpWebRequest Request { get; set; }
            public HttpWebResponse Response { get; set; }

            public byte[] Payload { get; set; }
            public int PayloadSize { get; set; }

            public HandlerContext() {
                Payload = new byte[128];
            }
        }

        public System.Func<Profile> onDone { get; private set; }

        public void CreateNewProfile(string json) {
            try {
                HandlerContext context = new HandlerContext();

                context.Payload = Encoding.UTF8.GetBytes(json);
                context.PayloadSize = json.Length;

                context.Request = (HttpWebRequest)WebRequest.Create(ServiceController.DebugAddr1);
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

        public void OnRequest(IAsyncResult ar) {
            try {
                HandlerContext context = (HandlerContext) ar.AsyncState;

                Stream stream = context.Request.EndGetRequestStream(ar);
                stream.Write(context.Payload, 0, context.PayloadSize);
                stream.Close();

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
                HandlerContext state = (HandlerContext) ar.AsyncState;
                state.Response = (HttpWebResponse) state.Request.EndGetResponse(ar);
                Stream stream = state.Response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string response = reader.ReadToEnd();

                Profile newProfile = JsonUtility.FromJson<Profile>(response);
                Debug.Log("created new profile: " + response);

                onDone = () => {
                    return newProfile;
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
