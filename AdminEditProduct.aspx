<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminEditProduct.aspx.cs" Inherits="TechStore.AdminEditProduct1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header" class="about-header">
        <h2>Edit Product Details</h2>
    </section>
    <section id="proddetails" class="section-p1">
    <div class="col-md-5">
        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                <li data-target="#carousel-example-generic" data-slide-to="4"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                        
                <asp:Repeater ID="rptrImages" runat="server">
                    <ItemTemplate>
                                
                        <div class="carousel-item <%# GetActiveClass(Container.ItemIndex) %>">
                                    
                                <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>" onerror="this.src='images/noimage.jpg'" style="width:100%">
                            </div>
                    </ItemTemplate>
                </asp:Repeater>
                            
            </div>
            <!-- Controls -->
            <a class="carousel-control-prev" href="#carousel-example-generic" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="carousel-control-next" href="#carousel-example-generic" role="button" data-slide="next">
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
        <br />
        <asp:Label ID="availablestock" runat="server" CssClass="text-muted"></asp:Label>
    </div>

    <div class="col-md-7" style="padding-left:20px;">
        <asp:Repeater ID="rptrProductDetails" runat="server">
            <ItemTemplate>
                  <div class="form-group">
                      <asp:Label ID="Label1" runat="server" Text="Product Name"></asp:Label>
                      <asp:TextBox ID="txtprodname" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label2" runat="server" Text="Product Brand"></asp:Label>
                      <asp:DropDownList ID="ddlBrand" CssClass="form-control" runat="server" ></asp:DropDownList>  
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label12" runat="server" Text="Product Category"></asp:Label>
                      <asp:DropDownList ID="ddlCat" CssClass="form-control" runat="server" ></asp:DropDownList>   
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label3" runat="server" Text="Product Quantity"></asp:Label>
                      <asp:TextBox ID="txtprodquan" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label4" runat="server" Text="Product Description"></asp:Label>
                      <asp:TextBox ID="txtproddes" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label5" runat="server" Text="Product Unit Price"></asp:Label>
                      <asp:TextBox ID="txtunitprice" CssClass="form-control" runat="server"></asp:TextBox>
                      <asp:RegularExpressionValidator Display="Static" EnableClientScript="false" ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtunitprice" ValidationExpression="^\\$?(([1-9](\\d*|\\d{0,2}(,\\d{3})*))|0)(\\.\\d{1,2})?$"></asp:RegularExpressionValidator>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label6" runat="server" Text="Product Usual Price"></asp:Label>
                      <asp:TextBox ID="txtusualprice" AutoPostBack="true" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="Label7" runat="server" Text="Product Discounted Price"></asp:Label>
                      <asp:TextBox ID="txtsellprice" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
                  <div class="form-group">
                      <asp:Label ID="lblProdDis" runat="server" Text="Product Discount"></asp:Label>
                      <asp:TextBox ID="txtproddis" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                  </div>
            <div class="form-group">
                <asp:Label ID="Label11" runat="server" Text="Upload Image 1"></asp:Label>
                <asp:FileUpload ID="fuImg01" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label ID="Label8" runat="server" Text="Upload Image 2"></asp:Label>
                 <asp:FileUpload ID="fuImg02" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label ID="Label9" runat="server" Text="Upload Image 3"></asp:Label>
                <asp:FileUpload ID="fuimg03" CssClass="form-control" runat="server" />
            </div>
            <div class="form-group">
                <asp:Label ID="Label10" runat="server" Text="Upload Image 4"></asp:Label>
                 <asp:FileUpload ID="fuimg04" CssClass="form-control" runat="server" />
            </div>
                
                <asp:Button ID="btnupdateprod" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnupdateprod_Click" />
                <asp:Label ID="lblMessage" CssClass="text-danger text-warning" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</section>
</asp:Content>
