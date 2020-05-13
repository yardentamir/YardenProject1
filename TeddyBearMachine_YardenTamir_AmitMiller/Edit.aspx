<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>מכונת הדובונים- עריכת משחק</title>
    <link rel="shortcut icon" type="image/png" href="images/GameIcon.png"/>
    <link rel="shortcut icon" type="image/png" href="http://example.com/favicon.png" />
    <link href="styles/StyleSheet1.css" rel="stylesheet" />
    <!--הקוד שמפעיל את תפריט הניווט-->
    <script src="jscrips/jquery-1.12.0.min.js"></script>
    <script src="jscrips/JavaScript.js"></script>
    <script src="jscrips/myScript.js"></script>
        <style type="text/css">
            .CharacterCount { 
            }
            </style>
    <script>
            function changeTextStatus(Button_id, Text_id) {
            if (document.getElementById(Text_id).value.length >= 1)
            {
                document.getElementById(Button_id).style["visibility"] = "hidden";
            }
            else
            {
                document.getElementById(Button_id).style["visibility"] = "visible";
            }
        }
    function changeButtonStatus(Text_id, Button_id,Delete_id) {
            if (document.getElementById(Button_id).value.length >= 1)
            {
                document.getElementById(Text_id).disabled = true;
                document.getElementById(Text_id).value = "";
                document.getElementById(Delete_id).style["visibility"] = "visible";
                //add pach
            }
            else
            {
                document.getElementById(Text_id).disabled = false;
                document.getElementById(Delete_id).style["visibility"] = "hidden";
            }
        }
    </script>
    <script type="text/javascript">
           function chooseFile(Inputid,Picture) {
       document.getElementById(Inputid).click();
       //document.getElementById(Picture).value = document.getElementById(Inputid).value;
       //document.getElementById(Picture).style["visibility"] = "visible";
   }
        function ChangeSavebtn() {
            if (document.getElementById('SaveQ').style.visibility != 'hidden') {
                if (document.getElementById('Question').value.length >= 2 &&
                    (document.getElementById('Ans0').value.length >= 2 || document.getElementById('fileInput2').value.length >= 1) &&
                    (document.getElementById('Ans1').value.length >= 2 || document.getElementById('fileInput3').value.length >= 1)) {
                    document.getElementById('SaveQ').disabled = false;
                }
                else {
                    document.getElementById('SaveQ').disabled = true;
                }
            } else {
                    if (document.getElementById('Question').value.length >= 2 &&
                    (document.getElementById('Ans0').value.length >= 2 || document.getElementById('fileInput2').value.length >= 1) &&
                    (document.getElementById('Ans1').value.length >= 2 || document.getElementById('fileInput3').value.length >= 1)) {
                    document.getElementById('UpdateBtn').disabled = false;
                }
                else {
                    document.getElementById('UpdateBtn').disabled = true;
                }
            }
            }
        function pachFunc(pach_Id,img_id,upload_id,uploadimg_id,Textbox_id) {
       document.getElementById(upload_id).value = "";
            document.getElementById(pach_Id).style["visibility"] = "hidden";
            document.getElementById(img_id).style["visibility"] = "hidden";
            document.getElementById(uploadimg_id).style["visibility"] = "visible";
            document.getElementById(img_id).value = "";
            document.getElementById(Textbox_id).disabled = false;

       //document.getElementById(Picture).value = document.getElementById(Inputid).value;
                }
                function addPachtoQpic(Button_id,Delete_id) {
                    if (document.getElementById(Button_id).value.length >= 1) {
                        document.getElementById(Delete_id).style["visibility"] = "visible";
                    } else {
                        document.getElementById(Delete_id).style["visibility"] = "hidden";
                    }
                }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
                <a href="login.aspx">
                    <img id="logo" src="images/GameIcon.png" />
                    <p>מכונת הדובונים</p>
                </a>
                <!--תפריט הניווט בראש העמוד-->
                <nav>
                    <ul>
                        <li><a class="about">אודות</a></li>
                        <li><a href="index.aspx">למשחק</a></li>
                    </ul>
                </nav>
            </header>
            <div id="aboutDiv" class="popUp bounceInDown hide">
            <a class="closeAbout">X</a>
                <h1>אודות</h1>
            <img id="about1" src="images/GameIcon.png" />
                <div id="meUpAbout">
                <h2>מכונת הדובונים</h2>
                <h3>אפיון ופיתוח: עמית מילר וירדן טמיר</h3>
                <p>אופיין ופותח במסגרת פרויקט בקורסים:
                    <br />
סביבות לימוד אינטראקטיביות 2 +  תכנות 2 + תכנות אינטראקטיבי 2
תשע"ט </p>
                <p><a href="https://www.hit.ac.il/telem/overview"> הפקולטה לטכנולוגיות למידה </a></p>
                <p>מכון טכנולוגי חולון</p>
                <img src="images/hitlogo.jpg" />
                    </div>
        </div>
            <div class="editdiv">
            <asp:Label ID="Label7" runat="server" class="titleq" Font-Size="XX-Large" Text="דף עריכה"></asp:Label>
                          <asp:Label ID="maagarQ" runat="server" class="titleq" Font-Size="XX-Large" Text="מאגר שאלות"></asp:Label>
            <br />
            <div class="grid-container">
                <div class="item1">
                                                <asp:Label ID="Label8" class="titleq" runat="server" Text="שם המשחק:"></asp:Label>
                            <asp:Label ID="gameID" runat="server" class="titleq" Text="Label"></asp:Label>
                    <br />
                    <asp:Button ID="NewQbtn" CssClass="DisButtons" runat="server" Text="שאלה חדשה" OnClick="NewQbtn_Click" />
<table>
    <tr>
        <th class="auto-style1">
            הקלד שאלה:      
        </th>
          <th>
<p style="color:red;">*</p>      
        </th>
        <th class="3">
            <asp:TextBox ID="Question" onkeyup="ChangeSavebtn()" item="1" CharacterLimit="80" CssClass="CharacterCount" runat="server" Height="32px" Width="244px"></asp:TextBox>
            </th>
        <th class="auto-style5">
            <asp:Label ID="LabelCounter1" runat="server" Text="0/80"></asp:Label>
        </th>
        <th>           
<div style="height:0px;overflow:hidden">
    <asp:FileUpload ID="fileInput" runat="server" onchange="addPachtoQpic(this.id,'Deletebtn0')"/>
</div>
    <asp:Image runat="server" id="picQimg" src="images/image_add.png" height="50" width="50" onclick="chooseFile('fileInput','ShowPic0');"/>
    <asp:ImageButton ID="ShowPic0" runat="server" style="visibility:hidden"  Height="50" Width="50" />
<asp:ImageButton ID="Deletebtn0" runat="server" style="visibility:hidden" ImageUrl="~/images/recycle-bin.png" Height="20" Width="20" OnClientClick="pachFunc(this.id,'ShowPic0','fileInput','picQimg','Question');return false;" />
        </th>
    </tr>
            <tr>
            <th>

            </th>
            <th>
                <br />
                <br />
            </th>
        </tr>
    <tr>
        <th>
אפשרויות תשובה
        </th>
   <th></th>
        <th colspan="3">
           <p style="color:red;">* יש למלא לפחות 2 תשובות כולל התשובה הנכונה </p> 
        </th>
    </tr>

  <tr>
      <th class="auto-style1">
        תשובה נכונה: 
      </th>
                <th>

                   <p style="color:red;">*</p> </th>
    <th class="auto-style3">
     &nbsp;<asp:TextBox ID="Ans0" item="2" CharacterLimit="70" onkeyup="ChangeSavebtn(); changeTextStatus('pic0', this.id)" CssClass="CharacterCount" runat="server" Width="210px" ></asp:TextBox>
                 
    </th>
      <th class="auto-style5">
             <asp:Label ID="LabelCounter2" runat="server" Text="0/70"></asp:Label>
      </th>
    <th>
        <div style="height:0px;overflow:hidden">
             <asp:FileUpload ID="fileInput2" runat="server" onchange="changeButtonStatus('Ans0', this.id,'Deletebtn1');ChangeSavebtn()"/>
</div>
    <asp:Image id="pic0" runat="server" src="images/image_add.png" height="50" width="50" onclick="chooseFile('fileInput2','ShowPic1')" />
            <asp:ImageButton ID="ShowPic1" runat="server" style="visibility:hidden" Height="50" Width="50" />
         <asp:ImageButton ID="Deletebtn1" runat="server" style="visibility:hidden" ImageUrl="~/images/recycle-bin.png" Height="20" Width="20"  OnClientClick="pachFunc(this.id,'ShowPic1','fileInput2','pic0','Ans0');return false;"/>
    </th>
  </tr>
<tr>
      <th class="auto-style1">
         תשובות שגויות:   
      </th>
              <th>
<p style="color:red;">*</p>    
        </th>
    <th >
                       <asp:TextBox ID="Ans1" item="3" CharacterLimit="70" onkeyup="ChangeSavebtn(); changeTextStatus('pic1', this.id);" CssClass="CharacterCount" runat="server" Width="210px"></asp:TextBox>
                   
    </th>
    <th>
         <asp:Label ID="LabelCounter3" runat="server" Text="0/70"></asp:Label>
    </th>
    <th> 
    
        <div style="height:0px;overflow:hidden">
  <asp:FileUpload ID="fileInput3" runat="server" onchange="changeButtonStatus('Ans1', this.id,'Deletebtn2');ChangeSavebtn()"/>
</div>
    <asp:Image runat="server" id="pic1" src="images/image_add.png" height="50" width="50" onclick="chooseFile('fileInput3','ShowPic2');"/>
    <asp:ImageButton ID="ShowPic2" runat="server" style="visibility:hidden" Height="50" Width="50" />
          <asp:ImageButton ID="Deletebtn2" runat="server" style="visibility:hidden" ImageUrl="~/images/recycle-bin.png" Height="20" Width="20"  OnClientClick="pachFunc(this.id,'ShowPic2','fileInput3','pic1','Ans1');return false;"/>
                    </th>
  </tr>
<tr>
      <th class="auto-style1"></th>
              <th></th>
    <th class="auto-style3">
                    <asp:TextBox ID="Ans2" item="4" CharacterLimit="70" CssClass="CharacterCount" runat="server" onkeyup="changeTextStatus('pic2', this.id)" Width="210px"></asp:TextBox>      
    </th>
    <th class="auto-style5">
           <asp:Label ID="LabelCounter4" runat="server" Text="0/70"></asp:Label>
    </th>
    <th> 
        <div style="height:0px;overflow:hidden">
 <asp:FileUpload ID="fileInput4" runat="server" onchange="changeButtonStatus('Ans2', this.id,'Deletebtn3')"/>
</div>
    <asp:Image runat="server" id="pic2" src="images/image_add.png" height="50" width="50" onclick="chooseFile('fileInput4','ShowPic3');"/>
    <asp:ImageButton ID="ShowPic3" runat="server" style="visibility:hidden" Height="50" Width="50"/>
      <asp:ImageButton ID="Deletebtn3" runat="server" style="visibility:hidden" ImageUrl="~/images/recycle-bin.png" Height="20" Width="20" OnClientClick="pachFunc(this.id,'ShowPic3','fileInput4','pic2','Ans2');return false;" />
    </th>

  </tr>
    <tr>
      <th class="auto-style1"></th>
                  <th>
 
        </th>
    <th class="3">
       <asp:TextBox ID="Ans3"  item="5" CharacterLimit="70" CssClass="CharacterCount"  runat="server"  Width="210px" onkeyup="changeTextStatus('pic3', this.id)"></asp:TextBox>
    </th>
        <th class="auto-style5">
<asp:Label ID="LabelCounter5" runat="server" Text="0/70"></asp:Label>
        </th>
    <th> 
          <div style="height:0px;overflow:hidden">
 <asp:FileUpload ID="fileInput5" runat="server" onchange="changeButtonStatus('Ans3', this.id,'Deletebtn4')"/>
</div>
    <asp:Image runat="server" id="pic3" src="images/image_add.png" height="50" width="50" onclick="chooseFile('fileInput5','ShowPic4');"/>
    <asp:ImageButton ID="ShowPic4" runat="server" style="visibility:hidden" Height="50" Width="50"/>
     <asp:ImageButton ID="Deletebtn4" runat="server" style="visibility:hidden" ImageUrl="~/images/recycle-bin.png" Height="20" Width="20" OnClientClick="pachFunc(this.id,'ShowPic4','fileInput5','pic3','Ans3');return false;" />
    </th>
  </tr> 
</table>
      <div class="tooltip">
      <asp:Button ID="SaveQ" runat="server" Text="שמור שאלה" CssClass="floatleft DisButtons" OnClick="SaveQ_Click" Visible="True" Enabled="False" />                                
  <span class="tooltiptext" > 
      אין אפשרות לשמור שאלה, כיון שדרוש מינימום 2 מסיחים לכל שאלה
  </span>
    </div>   
                    <asp:Button ID="UpdateBtn" runat="server" CssClass="floatleft DisButtons" Text="עדכן" OnClick="UpdateBtn_Click" Visible="False" />
                    <asp:Button ID="BackToGamesBtn" runat="server" Text="חזור" OnClick="BackToGamesBtn_Click" CssClass="floatleft; DisButtons" />
                </div>
                <div class="item2">
                    <asp:Label ID="Label9" style="margin-left:400px;" runat="server" Text="counter"></asp:Label>
<p style="color:red; margin-right:300px; margin-top:-19px;">לפחות 5 שאלות לפרסום</p>
                   
                    <asp:GridView ID="GridView2" CssClass="gridColor" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" Width="493px" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="שאלה">
                                <ItemTemplate>
                                    <asp:Label ID="NameLabel" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "@text").ToString())%>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="עריכה">
                                <ItemTemplate>
                                    <asp:ImageButton ID="settingsImageButton" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/wrench.png" Height="40" Width="40" CommandName="editRow" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="מחיקה">
                                <ItemTemplate>
                                    <asp:ImageButton ID="delete" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/recycle-bin.png" Height="40" Width="40" CommandName="deleteRow" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#84625a" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#010066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile1.xml"></asp:XmlDataSource>
                </div>
            </div>
                                                                        <asp:Panel ID="grayWindows" runat="server">
                <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp" runat="server">
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="ExitDeleteConf" CssClass="Exit DisButtons" runat="server" Text="כן" OnClick="ExitDeleteConf_Click1" Width="86px" />
                    <asp:Button ID="XnoXDeleteConf" CssClass="Exit DisButtons" runat="server" Text="לא" OnClick="ExitDeleteConf_Click1" Width="73px"/>
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="Label2" runat="server" Text="האם אתה בטוח/ה שברצונך למחוק את השאלה?"></asp:Label>
                </asp:Panel>
            </asp:Panel>
        </div>
            </div>
    </form>
</body>
</html>
