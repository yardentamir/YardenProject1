using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web.UI.HtmlControls;

public partial class Edit : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();
    string imagesLibPath = "uploadedFiles/";//הגדרת נתיב לשמירת הקובץ
    string imageNewName;
    string[] AnsImages = new string[4];

    protected void Page_init(object sender, EventArgs e)
    {
        myDoc.Load(MapPath("trees/XMLFile1.xml"));
        string theItemId = Session["theItemIdSession"].ToString();
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string theItemId = Session["theItemIdSession"].ToString();
        XmlDataSource1.XPath = "//games/game[@id='" + theItemId + "']//question";
        myDoc.Load(Server.MapPath("trees/XMLFile1.xml"));
        XmlNode node = myDoc.SelectSingleNode("/games/game[@id='" + theItemId + "']");
        gameID.Text = Server.UrlDecode(node.SelectSingleNode("name").InnerText);

        //for (int i = 0; i < 5; i++)
        //{
        //    ((ImageButton)FindControl("ShowPic" + i)).Style.Add("visibility", "hidden");
        //    ((ImageButton)FindControl("Deletebtn" + i)).Style.Add("visibility", "hidden");
        //}
        // סופר כמות שאלות
        int idcounter = 0;
        XmlNodeList myNodes;
        myNodes = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']//question");
        foreach (XmlNode myNode in myNodes)
        {
            idcounter++;
        }
            ((Label)FindControl("Label9")).Text = idcounter.ToString() +" שאלות ";
        SaveQ.Enabled = false;

        for(int i = 0; i < 4; i++)
        {
            ((TextBox)FindControl("Ans" + i)).Enabled = true;
        }
        
    }


    // כפתור עדכן
    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        string theItemId = Session["theItemIdSession"].ToString();
        string myID = Session["questionS"].ToString();


        if (((TextBox)FindControl("Question")).Text != "" || ((TextBox)FindControl("Ans0")).Text != "")
        {
            myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']").Attributes["text"].Value = Server.UrlEncode(((TextBox)FindControl("Question")).Text);
            //myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']/Ans[@cor='true']").InnerXml = Server.UrlEncode(((TextBox)FindControl("Ans0")).Text);
            
            //for (int i = 1; i < 4; i++)
            //{
            //    if(((TextBox)FindControl("Ans" + i)).Text != "")
            //    {
            //        myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']/Ans[@cor='false']").InnerXml = Server.UrlEncode(((TextBox)FindControl("Ans" + i)).Text);

            //        }
            }
            if (fileInput.HasFile)
        {

            string fileType = fileInput.PostedFile.ContentType;
            if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
            {
                // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                string fileName = fileInput.PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                // חיבור השם החדש עם הסיומת של הקובץ
                imageNewName = imagesLibPath + myTime + endOfFileName;
                //שמירה של הקובץ לספרייה בשם החדש שלו
                //FileUpload1.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);
                fileInput.PostedFile.SaveAs(Server.MapPath(imageNewName));
                //הצגה של הקובץ החדש מהספרייה
                ShowPic0.ImageUrl = imageNewName;

                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileInput.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 147, 110);

                //שמירה של הקובץ לספרייה בשם החדש שלו
                //objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);


                objImage.Save(Server.MapPath(imageNewName));
                myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']").Attributes["pic"].Value = imageNewName;

                myDoc.Save(Server.MapPath("/trees/XMLFile1.xml"));

            }
            else
            {
                // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
            }
        }
        else // אם אין תמונה בגזע השאלה
        {
            myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']").Attributes["pic"].Value = "False";

            myDoc.Save(Server.MapPath("/trees/XMLFile1.xml"));

        }





        ////---מסיחים-----////

        for (int i = 2; i < 6; i++)
        {
            int q = i - 2;
            //אם היתה לחיצה על העלאת תמונה בגזע השאלה
            if (((FileUpload)FindControl("fileInput" + i)).HasFile)
            {

                string fileType = ((FileUpload)FindControl("fileInput" + i)).PostedFile.ContentType;
                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = ((FileUpload)FindControl("fileInput" + i)).PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                    // חיבור השם החדש עם הסיומת של הקובץ
                    AnsImages[i - 2] = imagesLibPath + myTime + i + endOfFileName;
                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    //FileUpload1.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + AnsImages[i-2]);
                    ((FileUpload)FindControl("fileInput" + i)).PostedFile.SaveAs(Server.MapPath(AnsImages[i - 2]));
                    //הצגה של הקובץ החדש מהספרייה
                    ((ImageButton)FindControl("ShowPic" + (i - 1))).ImageUrl = AnsImages[i - 2];

                    Bitmap bmpPostedImage = new Bitmap(((FileUpload)FindControl("fileInput" + i)).PostedFile.InputStream);

                    //קריאה לפונקציה המקטינה את התמונה
                    //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                    System.Drawing.Image objImage = FixedSize(bmpPostedImage, 147, 110);

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    //objImage.Save(Server.MapPath(imagesLibPath) + AnsImages[i-2]);
                    objImage.Save(Server.MapPath(AnsImages[i - 2]));
                    // XmlElement Answer = myDoc.CreateElement("Ans");
                    //XmlText Anst = myDoc.CreateTextNode(AnsImages[i - 2]);
                    myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']//Ans").InnerText = AnsImages[i - 2];
                    myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']//Ans").Attributes["anstype"].Value = "pic";
                    //Answer.SetAttribute("anstype", "pic");
                    myDoc.Save(Server.MapPath("trees/XMLFile1.xml"));


                }
                else
                {
                    // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                }
            }
            else  // יש במסיחים טקסט
            {
                if (((TextBox)FindControl("Ans" + q)).Text != "") { 
                //{
                //XmlDocument Document = XmlDataSource1.GetXmlDocument();
                // XmlNode TextAns = myDoc.SelectSingleNode("/games/game[@id='" + theItemId + "']/question[@id='" + myID + "']//Ans").InnerXml = ((TextBox)FindControl("Ans" + q)).Text;
                //if (((TextBox)FindControl("Question")).Text != "" || ((TextBox)FindControl("Ans0")).Text != "")
                //{
                    myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']").Attributes["text"].Value = Server.UrlEncode(((TextBox)FindControl("Question")).Text);
                    //myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']/Ans[@cor='true']").InnerXml = Server.UrlEncode(((TextBox)FindControl("Ans0")).Text);

                        if (((TextBox)FindControl("Ans" + q)).Text != "")
                        {
                            myDoc.SelectSingleNode("//games/game[@id='" + theItemId + "']/question[@id='" + myID + "']//Ans").InnerXml = Server.UrlEncode(((TextBox)FindControl("Ans" + q)).Text);
                        }
               // }
                }
                myDoc.Save(Server.MapPath("trees/XMLFile1.xml"));
            }
            if (((FileUpload)FindControl("fileInput" + i)).PostedFile.FileName == "" && ((TextBox)FindControl("Ans" + q)).Text == "")
            {
                XmlNode nodeAns = myDoc.SelectSingleNode("/games/game[@id='" + theItemId + "']/question[@id='" + myID + "']//Ans");
                nodeAns.ParentNode.RemoveChild(nodeAns);
            }
        }
            myDoc.Save(Server.MapPath("trees/XMLFile1.xml"));
            ((FileUpload)FindControl("fileInput")).Dispose();
            ((TextBox)FindControl("Question")).Text = "";
            for (int i = 0; i < 4; i++)
            {
                ((TextBox)FindControl("Ans" + i)).Text = "";
                ((System.Web.UI.WebControls.Image)FindControl("pic" + i)).Style.Add("visibility", "visible");

            }
            for (int i = 2; i < 6; i++)
            {
                ((FileUpload)FindControl("fileInput" + i)).Dispose();
            }
            SaveQ.Visible = true;
            UpdateBtn.Visible = false;

            for (int i = 0; i < 5; i++)
            {
                ((ImageButton)FindControl("ShowPic" + i)).ImageUrl = "";
                ((ImageButton)FindControl("ShowPic" + i)).Style.Add("visibility", "hidden");
                ((ImageButton)FindControl("Deletebtn" + i)).Style.Add("visibility", "hidden");

            }
        ((System.Web.UI.WebControls.Image)FindControl("picQimg")).Style.Add("visibility", "visible");
            GridView2.DataBind();
        

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
       string theId = i.Attributes["theItemId"];

        Session["questionS"] = theId;
        string myID = Session["questionS"].ToString();
        string theItemId = Session["theItemIdSession"].ToString();

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":

                ////הצגה של המסך האפור
                grayWindows.Style.Add("display", "block");
                ////הצגת הפופ-אפ של המחיקה
                DeleteConfPopUp.Style.Add("display", "block");
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                //string Idb = Session["isEdit"].ToString();
                //Session["isEdit"] = "edit";
                ((TextBox)FindControl("Question")).Text = "";
                ((TextBox)FindControl("Ans0")).Text = "";
                for (int q = 1; q < 4; q++)
                {
                    ((TextBox)FindControl("Ans" + q)).Text = "";
                }
                showQ(theId);
                SaveQ.Visible = false;
                UpdateBtn.Visible = true;
                break;
        
        }
    }
    protected void showQ(string myId)
    {
        //((FileUpload)FindControl("fileInput")).Dispose();
        //for (int i = 0; i < 5; i++)
        //{
        //    //((ImageButton)FindControl("ShowPic" + i)).ImageUrl = "";
        //    ((ImageButton)FindControl("Deletebtn" + i)).Style.Add("visibility", "hidden");
        //    ((ImageButton)FindControl("ShowPic" + i)).Style.Add("visibility", "hidden");
        //}
        //for (int i = 2; i < 6; i++)
        //{
        //    ((FileUpload)FindControl("fileInput" + i)).Dispose();
        //}
        for (int i = 0; i < 4; i++)
        {
            ((System.Web.UI.WebControls.Image)FindControl("pic" + i)).Style.Add("visibility", "visible");
        }
        ((System.Web.UI.WebControls.Image)FindControl("picQimg")).Style.Add("visibility", "visible");

        string theItemId = Session["theItemIdSession"].ToString();
        XmlNodeList myNodes0;
        myNodes0 = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']/question[@id='" + myId + "']");
        foreach (XmlNode myNode in myNodes0)
        {
            ((TextBox)FindControl("Question")).Text = Server.UrlDecode(myNode.Attributes["text"].Value);

            if (myNode.Attributes["pic"].Value != "False")
            {
                ((ImageButton)FindControl("ShowPic0")).ImageUrl = myNode.Attributes["pic"].Value;
                ((FileUpload)FindControl("fileInput")).PostedFile.SaveAs(Server.MapPath(myNode.Attributes["pic"].Value)); 
                ((ImageButton)FindControl("ShowPic0")).Style.Add("visibility", "visible");
                ((System.Web.UI.WebControls.Image)FindControl("picQimg")).Style.Add("visibility", "hidden");
                ((ImageButton)FindControl("Deletebtn0")).Style.Add("visibility", "visible");
            }
        }

        // הצגת מסיחים
        XmlNodeList myNodes6 = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']/question[@id='" + myId + "']//Ans");
        int counterp2 = 1;
        int forfilesCounter = 2;
        int uploadIconCounter = 0;
        foreach (XmlNode myNode6 in myNodes6)
        {
            if (myNode6.Attributes["anstype"].Value == "pic")
            {
                ((ImageButton)FindControl("ShowPic" + counterp2)).ImageUrl = myNode6.InnerXml;
                ((FileUpload)FindControl("fileInput" + forfilesCounter)).SaveAs(Server.MapPath(myNode6.InnerText));
                ((ImageButton)FindControl("ShowPic" + counterp2)).Style.Add("visibility", "visible");
                ((System.Web.UI.WebControls.Image)FindControl("pic" + uploadIconCounter)).Style.Add("visibility", "hidden");
                ((TextBox)FindControl("Ans" + uploadIconCounter)).Enabled = false;
                ((ImageButton)FindControl("Deletebtn"+ counterp2)).Style.Add("visibility", "visible");
            }
            else  /// כלומר טקסט
            {
                ((TextBox)FindControl("Ans" + uploadIconCounter)).Text = Server.UrlDecode(myNode6.InnerXml);
                ((ImageButton)FindControl("ShowPic" + counterp2)).Style.Add("visibility", "hidden");
                ((ImageButton)FindControl("Deletebtn" + uploadIconCounter)).Style.Add("visibility", "hidden");
                ((System.Web.UI.WebControls.Image)FindControl("pic" + uploadIconCounter)).Style.Add("visibility", "hidden");
            }
            counterp2++;
            forfilesCounter++;
            uploadIconCounter++;
        }



          

        //XmlNodeList myNodes2;

        //myNodes2 = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']/question[@id='" + myId + "']/Ans[@cor='false']");
        //int counter = 0;
        //foreach (XmlNode myNode in myNodes2)
        //{
        //    ((TextBox)FindControl("Ans" + counter)).Text = Server.UrlDecode(myNode.InnerXml);
        //    counter++;
        //}

    }

    void deleteRow(string theItemId, string theId)
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/games/game[@id='" + theItemId + "']/question[@id='" + theId + "']");
        node.ParentNode.RemoveChild(node);
        XmlDataSource1.Save();
        GridView2.DataBind();

        ((TextBox)FindControl("Question")).Text = "";
        ((TextBox)FindControl("Ans0")).Text = "";
        for (int i = 1; i < 4; i++)
        {
            ((TextBox)FindControl("Ans" + i)).Text = "";
        }
        SaveQ.Visible = true;
        UpdateBtn.Visible = false;
        int idcounter = -1;
        XmlNodeList myNodes;
        myNodes = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']//question");
        foreach (XmlNode myNode in myNodes)
        {
            idcounter++;
        }
            ((Label)FindControl("Label9")).Text = idcounter.ToString() + " שאלות ";

    }



    protected void SaveQ_Click(object sender, EventArgs e)
    {
        /*
         * איף לכל תמונה || איף לכל טקסטבוקס
         * בכל איף לקחת את הערך הנכון - תמונה או טקסט ולהכניס לעץ
         * העלאת כל התמונות בלחיצת הכפתור
         */
        string theItemId = Session["theItemIdSession"].ToString();
        XmlNode counterNode = myDoc.SelectSingleNode("/games/game[@id=" + theItemId + "]");
        //

        int idCounter = 1;
        XmlNodeList myNodes;
        myNodes = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']//question");
        foreach (XmlNode myNode in myNodes)
        {
            idCounter++;
        }
        //
         //< question id = "1" text = "%d7%94%d7%99%d7%95%d7%a9" trys = "1" pic = "False" ifwas = "false" >
                 XmlElement newQ = myDoc.CreateElement("question");
        newQ.SetAttribute("id", idCounter.ToString());


        //אם היתה לחיצה על העלאת תמונה בגזע השאלה
        if (fileInput.HasFile)
        {

            string fileType = fileInput.PostedFile.ContentType;
            if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
            {
                // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                string fileName = fileInput.PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                // חיבור השם החדש עם הסיומת של הקובץ
                imageNewName = imagesLibPath + myTime + endOfFileName;
                //שמירה של הקובץ לספרייה בשם החדש שלו
                //FileUpload1.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);
                fileInput.PostedFile.SaveAs(Server.MapPath(imageNewName));
                //הצגה של הקובץ החדש מהספרייה
                ShowPic0.ImageUrl = imageNewName;

                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileInput.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 147, 110);

                //שמירה של הקובץ לספרייה בשם החדש שלו
                //objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);

                objImage.Save(Server.MapPath(imageNewName));
                    newQ.SetAttribute("text", Server.UrlEncode(((TextBox)FindControl("Question")).Text));
                   newQ.SetAttribute("trys", Convert.ToString(1));
                    newQ.SetAttribute("pic", imageNewName);
                    newQ.SetAttribute("ifwas", "false");
                counterNode.AppendChild(newQ);

                myDoc.Save(Server.MapPath("/trees/XMLFile1.xml"));

            }
            else
            {
                // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
            }
        }
        else // אם אין תמונה בגזע השאלה
        {
            newQ.SetAttribute("text", Server.UrlEncode(((TextBox)FindControl("Question")).Text));
            newQ.SetAttribute("trys", Convert.ToString(1));
            newQ.SetAttribute("pic", "False");
            newQ.SetAttribute("ifwas", "false");
            counterNode.AppendChild(newQ);

            myDoc.Save(Server.MapPath("/trees/XMLFile1.xml"));

        }





        ////---מסיחים-----////

        for(int i=2; i < 6; i++)
        {
            //אם היתה לחיצה על העלאת תמונה בגזע השאלה
            if (((FileUpload)FindControl("fileInput"+i)).HasFile)
            {

                string fileType = ((FileUpload)FindControl("fileInput" + i)).PostedFile.ContentType;
                if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
                {
                    // הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                    string fileName = ((FileUpload)FindControl("fileInput" + i)).PostedFile.FileName;
                    // הסיומת של הקובץ
                    string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                    string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");

                    // חיבור השם החדש עם הסיומת של הקובץ
                    AnsImages[i-2] = imagesLibPath + myTime + i + endOfFileName;
                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    //FileUpload1.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + AnsImages[i-2]);
                    ((FileUpload)FindControl("fileInput" + i)).PostedFile.SaveAs(Server.MapPath(AnsImages[i - 2]));
                    //הצגה של הקובץ החדש מהספרייה
                    ((ImageButton)FindControl("ShowPic"+(i-1))).ImageUrl = AnsImages[i - 2];

                    Bitmap bmpPostedImage = new Bitmap(((FileUpload)FindControl("fileInput" + i)).PostedFile.InputStream);

                    //קריאה לפונקציה המקטינה את התמונה
                    //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                    System.Drawing.Image objImage = FixedSize(bmpPostedImage, 147, 110);

                    //שמירה של הקובץ לספרייה בשם החדש שלו
                    //objImage.Save(Server.MapPath(imagesLibPath) + AnsImages[i-2]);
                    objImage.Save(Server.MapPath(AnsImages[i - 2]));
                    XmlElement Answer = myDoc.CreateElement("Ans");
                    XmlText Anst = myDoc.CreateTextNode(AnsImages[i - 2]);
                    if (i == 2)
                    {
                        Answer.SetAttribute("cor", "true");
                    }
                    else
                    {
                        Answer.SetAttribute("cor", "false");
                    }
                    Answer.SetAttribute("anstype", "pic");
                    Answer.AppendChild(Anst);
                    newQ.AppendChild(Answer);
                    myDoc.Save(Server.MapPath("trees/XMLFile1.xml"));


                }
                else
                {
                    // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                }
            }
            else  // יש במסיחים טקסט
            {
                int q = i - 2;
                if (((TextBox)FindControl("Ans" + q)).Text != "")
                {
                    XmlElement Answer = myDoc.CreateElement("Ans");
                    XmlText anst2 = myDoc.CreateTextNode(Server.UrlEncode(((TextBox)FindControl("Ans" + q)).Text));
                    if (q == 0)
                    {
                        Answer.SetAttribute("cor", "true");
                    }
                    else
                    {
                        Answer.SetAttribute("cor", "false");
                    }
                    Answer.SetAttribute("anstype", "txt");
                    Answer.AppendChild(anst2);
                    newQ.AppendChild(Answer);
                    myDoc.Save(Server.MapPath("trees/XMLFile1.xml"));
                }
            }


            GridView2.DataBind();
            //----------------------------------------------------//
            int idcounter = 0;
            XmlNodeList myNodes12;
            myNodes12 = myDoc.SelectNodes("//games/game[@id='" + theItemId + "']//question");
            foreach (XmlNode myNode in myNodes12)
            {
                idcounter++;
            }
            ((Label)FindControl("Label9")).Text = idcounter.ToString() + " שאלות ";

        }
        ((FileUpload)FindControl("fileInput")).Dispose();
        ((TextBox)FindControl("Question")).Text = "";
        for (int i = 0; i < 4; i++)
        {
            ((TextBox)FindControl("Ans" + i)).Text = "";
            
        }
        for(int i = 2; i < 6; i++)
        {
            ((FileUpload)FindControl("fileInput" + i)).Dispose();
        }


    }




    // פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }


    protected void ExitDeleteConf_Click1(object sender, EventArgs e)
    {
        string myID = Session["questionS"].ToString();
        Button myExitBtn = (Button)sender;
        //קבלת השם של הכפתור
        string ButtonID = myExitBtn.ID;
        //מתוך השם של הכפתור אנו לוקחים את השם ללא התחילית exit
        string PopUpID = ButtonID.Substring(4);

        //מציאת הפאנל של החלון הנפתח וסגירתו
        ((Panel)FindControl(PopUpID + "PopUp")).Style.Add("display", "none");
        //סגירת הרקע האפור
        grayWindows.Style.Add("display", "none");
        if (ButtonID == "ExitDeleteConf")
        {
            deleteRow((string)Session["theItemIdSession"],myID);
        }


    }

    protected void BackToGamesBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CeateGames.aspx");
    }


    protected void NewQbtn_Click(object sender, EventArgs e)
    {
        ((FileUpload)FindControl("fileInput")).Dispose();
        ((TextBox)FindControl("Question")).Text = "";
        for (int i = 0; i < 4; i++)
        {
            ((TextBox)FindControl("Ans" + i)).Text = "";
            ((System.Web.UI.WebControls.Image)FindControl("pic" + i)).Style.Add("visibility", "visible");

        }
        for (int i = 2; i < 6; i++)
        {
            ((FileUpload)FindControl("fileInput" + i)).Dispose();
        }
        SaveQ.Visible = true;
        UpdateBtn.Visible = false;

        for(int i = 0; i < 5; i++)
        {
            ((ImageButton)FindControl("ShowPic" + i)).ImageUrl = "";
            ((ImageButton)FindControl("ShowPic" + i)).Style.Add("visibility", "hidden");
            ((ImageButton)FindControl("Deletebtn"+i)).Style.Add("visibility", "hidden");

        }
        ((System.Web.UI.WebControls.Image)FindControl("picQimg")).Style.Add("visibility", "visible");
    }

    //protected void Deletebtn_Click(object sender, ImageClickEventArgs e)
    //{
    //    ImageButton DeleteBtn = (ImageButton)sender;
    //    //קבלת השם של הכפתור
    //    string DeleteBtnID = DeleteBtn.ID;
    //    //מתוך השם של הכפתור אנו לוקחים את השם ללא התחילית exit
    //    string PachID = DeleteBtnID.Substring(9);
    //    //מציאת הפאנל של החלון הנפתח וסגירתו
    //    ((ImageButton)FindControl("Deletebtn"+ PachID)).Style.Add("visibility", "visible");
    //    ((ImageButton)FindControl("ShowPic" + PachID)).ImageUrl = "";
    //    ((ImageButton)FindControl("ShowPic" + PachID)).Style.Add("visibility", "hidden");
    //    if(Convert.ToInt16(PachID) == 0)
    //    {
    //        ((System.Web.UI.WebControls.Image)FindControl("picQimg")).Style.Add("visibility", "visible");
    //    }else
    //    {
    //        ((System.Web.UI.WebControls.Image)FindControl("pic"+ (Convert.ToInt16(PachID) - 1))).Style.Add("visibility", "visible");
    //    }
    //}
}