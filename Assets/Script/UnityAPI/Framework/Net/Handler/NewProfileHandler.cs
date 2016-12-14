using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityAPI.Framework.Net {
    public class NewProfileHandler : ClientServiceHandler {
        // public static void POSTasync(string json, Func<string> getNameCb, Func<bool> isValidCb) {
        //     try {
        //         IHandler state = new ProfileNameVerificationHandler();
        //         state.onComplete = getNameCb;

        //         state.Request = (HttpWebRequest) WebRequest.Create(debug_route);
        //         state.Request.ContentType = "application/json; charset=utf-8";
        //         state.Request.Method = kPOST;

        //         state.Payload = Encoding.UTF8.GetBytes(json);
        //         state.PayloadSize = json.Length;

        //         Debug.LogFormat("making async request, payload size: {0}", state.PayloadSize);

        //         state.Request.BeginGetRequestStream(
        //             new AsyncCallback(POSTrequestStreamCallback),
        //             state
        //         );
        //     } catch (WebException we) {
        //         Debug.Log(NetUtil.PrintfWebException(we));
        //     } catch (Exception e) {
        //         Debug.Log(NetUtil.PrintfException(e));
        //     }
        // }

        // private static void POSTrequestStreamCallback(IAsyncResult ar) {
        //     try {
        //         IConnectionState state = (IConnectionState) ar.AsyncState;

        //         state.RWStream = state.Request.EndGetRequestStream(ar);
        //         state.RWStream.Write(state.Payload, 0, state.PayloadSize);
        //         state.RWStream.Close();

        //         state.Request.BeginGetResponse(
        //             new AsyncCallback(POSTresponseCallback),
        //             state
        //         );
        //     } catch (WebException we) {
        //         Debug.Log(NetUtil.PrintfWebException(we));
        //     } catch (Exception e) {
        //         Debug.Log(NetUtil.PrintfException(e));
        //     }
        // }

        // private static void POSTresponseCallback(IAsyncResult ar) {
        //     try {
        //         IConnectionState state = (IConnectionState) ar.AsyncState;
        //         state.Response = (HttpWebResponse) state.Request.EndGetResponse(ar);
        //         state.RWStream = state.Response.GetResponseStream();

        //         StreamReader reader = new StreamReader(state.RWStream);
        //         string response = reader.ReadToEnd();

        //         state.onComplete = () => { return response; };

        //         reader.Close();
        //         state.RWStream.Close();
        //         state.Response.Close();
        //     } catch (WebException we) {
        //         Debug.Log(NetUtil.PrintfWebException(we));
        //     } catch (Exception e) {
        //         Debug.Log(NetUtil.PrintfException(e));
        //     }
        // }
    }
}
