<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CeateGames.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>מכונת הדובונים- יצירת משחק</title>
    <link rel="shortcut icon" type="image/png" href="images/GameIcon.png" />
    <link href="styles/StyleSheet1.css" rel="stylesheet" />
    <%--scrips--%>    
    <!--הקוד שמפעיל את תפריט הניווט-->
    <script src="jscrips/jquery-1.12.0.min.js"></script>
    <script src="jscrips/JavaScript.js"></script>
        <script type="text/javascript">
        function changeButtonStatus() {
            if (document.getElementById('<% = TextBox1.ClientID %>').value.length >= 2)
            {
                document.getElementById('<% = ImageButton1.ClientID %>').disabled = false;
            }
            else
            {
                document.getElementById('<% = ImageButton1.ClientID %>').disabled = true;
            }

        }
    </script>
    <style type="text/css">
        .CharacterCount {
        }
    </style>
    <!--הקוד שמפעיל את תפריט הניווט-->

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

            <div id="Container">
                <h4>&nbsp;הזן שם למשחק חדש (2-50 תווים)</h4>
                <asp:TextBox ID="TextBox1" item="1" CharacterLimit="50" CssClass="CharacterCount" runat="server" Height="37px" Width="368px" onkeyup="changeButtonStatus(ImageButton1)" TextMode="MultiLine"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  

       <%--   כפתור יצירת משחק--%> <asp:Button ID="ImageButton1" CssClass="DisButtons" runat="server" Text="צור משחק" OnClick="Button1_Click" Enabled="false"/>
                <br />
                <asp:Label ID="LabelCounter1" runat="server" Text="0/50"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" CssClass="gridColor" runat="server" DataSourceID="XmlDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" Height="107px" Width="957px">
                    <Columns>
                        <asp:TemplateField HeaderText="שם משחק">
                            <ItemTemplate>
                                <asp:Label ID="NameLabel" runat="server" CssClass="cenetrq" Text='<%# Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "name").ToString())%>'> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="קוד משחק">
                            <ItemTemplate>
                                <asp:Label ID="idLabel" runat="server" CssClass="cenetrq"  Text='<%#XPathBinder.Eval(Container.DataItem, "@id")%>'> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="שאלות">
                            <ItemTemplate>
                                <asp:Label ID="QuestionLBL" CssClass="cenetrq" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "count(question)")%>'> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="הגדרות">
                            <ItemTemplate>
                                <asp:ImageButton ID="settingsImageButton" CssClass="cenetrq" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/wrench.png" Height="40" Width="40" CommandName="settingsRow" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="עריכה">
                            <ItemTemplate>
                                <asp:ImageButton ID="editImageButton" CssClass="cenetrq" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/pencil.png" Height="40" Width="40" CommandName="editRow" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מחיקה">
                            <ItemTemplate>
                                <asp:ImageButton ID="deleteImageButton" CssClass="cenetrq" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/recycle-bin.png" Height="40" Width="40" CommandName="deleteRow" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="פרסום">
                            <ItemTemplate>
                                  <div class="tooltip">
                                       <asp:CheckBox ID="isPassCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="isPassCheckBox_CheckedChanged1" Class='<%#CheckIfCanPublish(XPathBinder.Eval(Container.DataItem,"@id").ToString())%>' Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@isPub"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' />
  <span class="tooltiptext" > 
      דרוש לפחות 5 שאלות לפרסום
  </span>
    </div>                  
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
                <br />
                <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile1.xml" XPath="//games/game"></asp:XmlDataSource>
            </div>
            <asp:Panel ID="grayWindows" runat="server">
                <!-- פופ-אפ למחיקה - כאן אפשר להוסיף את הפקדים הרלוונטים -->
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUp" runat="server">
                    <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="ExitDeleteConf" CssClass="Exit DisButtons" runat="server" Text="כן" OnClick="ExitDeleteConf_Click1" Width="50px" />
                     &nbsp;&nbsp; 
                    <asp:Button ID="XnoXDeleteConf" CssClass="Exit DisButtons" runat="server" Text="לא" OnClick="ExitDeleteConf_Click1" Width="50px"/>
                    <!-- תווית להצגת הודעה למשתמש -->
                    <asp:Label ID="Label1" runat="server" Text="האם אתה בטוח/ה שברצונך למחוק את המשחק?"></asp:Label>
                </asp:Panel>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

