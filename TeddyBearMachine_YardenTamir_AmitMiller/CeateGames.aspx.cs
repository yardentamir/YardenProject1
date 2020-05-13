using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void isPassCheckBox_CheckedChanged1(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        CheckBox myCheckBox = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];
        XmlNode theGames = xmlDoc.SelectSingleNode("/games/game[@id=" + theId + "]");
        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPub = myCheckBox.Checked;
        //myGame.Attributes["isPub"].InnerText = "true";
        //עדכון של המאפיין בעץ
        theGames.Attributes["isPub"].InnerText = NewIsPub.ToString();
        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdSession"] = i.Attributes["theItemId"];
        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                //הצגה של המסך האפור
                grayWindows.Style.Add("display", "block");
                //הצגת הפופ-אפ של המחיקה
                DeleteConfPopUp.Style.Add("display", "block");
                //deleteRow(theId);
                
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":

                Response.Redirect("Edit.aspx");
                break;
            case "settingsRow":

                Response.Redirect("Settings.aspx");
                break;
        }


    }

    //מחיקת משחק
    void deleteRow(string theItemId)
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/games/game[@id='" + theItemId + "']");
        node.ParentNode.RemoveChild(node);
        XmlDataSource1.Save();
        GridView1.DataBind();

    }

    //שיטה שמקבלת את מספר המשחק ובודקת האם המשחק עומד בתנאי הפרסום
    //במידה ולא מחיזרה את המחרוזת disabled

    public string CheckIfCanPublish(string id)
    {
        int questionNum = 1;
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument(); //טעינה של העץ
        XmlNode myGame = xmlDoc.SelectSingleNode("//games/game[@id=" + id + "]"); //קבלת המשחק שאנו רוצים

        bool canPublish = false; //משתנה בוליאני - כברירת מחדל - לא ניתן לפרסום
        string disCalss = ""; //משתנה ל class
        
        ////// פה נוסיף את הקוד לבדיקה של התנאי שאנחנו רוצים  
        
        XmlNodeList myNodes;
        myNodes = xmlDoc.SelectNodes("//games/game[@id='" + id + "']//question");
        foreach (XmlNode myNode in myNodes)
        {
            questionNum++;
        }
        if (questionNum > 5)
        {
            canPublish = true;
            disCalss = "enabled";
            //myGame.Attributes["isPub"].InnerText = "true";
            XmlDataSource1.Save();
        }
        //// במידה והמשחק עומד בתנאי הפרסום נשנה את המשתנה הבוליאני ל                true

        //במידה ולא ניתן לפרסם
        if (canPublish == false)
        {
            myGame.Attributes["isPub"].InnerText = "false"; //במידה ולא ניתן לפרסם יש להגדיר בעץ כfalse
            XmlDataSource1.Save();
            disCalss = "disabled"; //הוספת קלאס של לא מאופשר – שלב 2
            
        }
        return disCalss;

    }



    protected void ExitDeleteConf_Click1(object sender, EventArgs e)
    {

        Button myExitBtn = (Button)sender;
        //קבלת השם של הכפתור
        string ButtonID = myExitBtn.ID;
        //מתוך השם של הכפתור אנו לוקחים את השם ללא התחילית exit
        string PopUpID = ButtonID.Substring(4);

        //מציאת הפאנל של החלון הנפתח וסגירתו
        ((Panel)FindControl(PopUpID + "PopUp")).Style.Add("display", "none");
        //סגירת הרקע האפור
        grayWindows.Style.Add("display", "none");
        if(ButtonID == "ExitDeleteConf")
        {
        deleteRow((string)Session["theItemIdSession"]);
        }
        

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerXml = myNewId;

        XmlElement myNewGameNode = xmlDoc.CreateElement("game");
        myNewGameNode.SetAttribute("id", myNewId);
        myNewGameNode.SetAttribute("isPub", "False");
        myNewGameNode.SetAttribute("time", "30");
        //myNewGameNode.SetAttribute("name", Server.UrlEncode(TextBox1.Text));

        XmlElement mynameNode = xmlDoc.CreateElement("name");
        mynameNode.InnerXml = Server.UrlEncode(TextBox1.Text);
        myNewGameNode.AppendChild(mynameNode);

        XmlNode FirstGame = xmlDoc.SelectNodes("/games/game").Item(0);
        xmlDoc.SelectSingleNode("/games").InsertBefore(myNewGameNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();
        //ניקוי תיבת טקסט
        TextBox1.Text = "";
    }
}