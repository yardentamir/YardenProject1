<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("~/trees/XMLFile1.xml"));


        string gameCode = context.Request["gameCode"]; // חשוב לשים לב שזה אותו שם משתנה כמו באנימייט

        XmlNode gameNode = myDoc.SelectSingleNode("//game[@id='" + gameCode + "']");


        if (gameNode != null)
        {
            string isPublish = gameNode.Attributes["isPub"].Value.ToString();
            if (isPublish == "False")
            {
                context.Response.Write("המשחק לא פורסם");
            }
            else
            {
                string jsonText = JsonConvert.SerializeXmlNode(gameNode);
                    context.Response.Write(jsonText);
            }
        }
        else
        {
            context.Response.Write("לא נמצא משחק");
        }

    }

    public bool IsReusable

    {
        get
        {
            return true;
        }
    }
}

















