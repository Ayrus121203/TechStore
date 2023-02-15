<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="TechStore.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
        <div class="form-horizontal">
            <h2>Add Product</h2>
            <hr />
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-md-2 control-label" Text="Product Name"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtPName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label20" runat="server" CssClass="col-md-2 control-label" Text="Product Quantity"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" CssClass="col-md-2 control-label" Text="Product Brand"></asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlBrand" CssClass="form-control" runat="server" ></asp:DropDownList>      
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label10" runat="server" CssClass="col-md-2 control-label" Text="Product Category"></asp:Label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlCat" CssClass="form-control" runat="server" ></asp:DropDownList>      
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" CssClass="col-md-2 control-label" Text="Product Unit Price"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtunitprice" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label8" runat="server" CssClass="col-md-2 control-label" Text=" Product Description"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtDesc" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="col-md-2 control-label" Text="Product Usual Selling Price"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtusualprice" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="col-md-2 control-label" Text="Product Discounted Price"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtSelPrice" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="lblProdDis" CssClass="col-md-2 control-label" runat="server" Text="Product Discount"></asp:Label>
                        <div class="col-md-8">
                        <asp:TextBox ID="txtproddis" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label11" runat="server" CssClass="col-md-2 control-label" Text="Upload Image 1"></asp:Label>
                        <div class="col-md-8">
                            <asp:FileUpload ID="fuImg01" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" CssClass="col-md-2 control-label" Text="Upload Image 2 (Optional)"></asp:Label>
                        <div class="col-md-8">
                         <asp:FileUpload ID="fuImg02" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label6" runat="server" CssClass="col-md-2 control-label" Text="Upload Image 3 (Optional)"></asp:Label>
                        <div class="col-md-8">
                            <asp:FileUpload ID="fuImg03" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <div class="form-group">
                        <asp:Label ID="Label9" runat="server" CssClass="col-md-2 control-label" Text="Upload Image 4 (Optional)"></asp:Label>
                        <div class="col-md-8">
                         <asp:FileUpload ID="fuImg04" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col">

                </div>
            </div>
            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
            <asp:Label ID="lblMessage" CssClass="text-danger text-warning" runat="server"></asp:Label>
       </div>
    </div>
</asp:Content>
