<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="CheckoutCart.aspx.cs" Inherits="TechStore.CheckoutCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header" class="about-header">
    </section>

    <section id="cart" class="section-p1">
        <table width="100%">
            <thead>
                <tr>
                    <td>Image</td>
                    <td>Product</td>
                    <td>Price</td>
                    <td>Quantity</td>
                    <td>Subtotal</td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptrCartProds" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td> <img src="Images/ProductImages/<%#Eval("ProductID") %>/<%#Eval("Name") %><%#Eval("Extension") %>" alt="<%#Eval("Name") %>"></td>
                            <td><%#Eval("Name") %></td>
                            <td>$ <%#Eval("ProdSellPrice") %></td>
                            <td><%#Eval("CartProdQuantity") %></td>
                            <td>$ <%#Eval("CartProdPrice") %></td>
                        </tr>                  
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </section>

    <section id="cart-add" class="section-p1">
        <div id="deliveryinfo">
            <h3>Delivery Information</h3>
            <table>
                <tr>
                    <td>Delivery Address</td>
                    <td id="deliveryaddress" runat="server"></td>
                </tr>
                <tr>
                    <td>Name Of Buyer</td>
                    <td runat="server" id="nameofbuyer"></td>
                </tr>
                <tr>
                    <td>Contact Number</td>
                    <td runat="server" id="contactnum"></td>
                </tr>
            </table>
        </div>

        <div id="subtotal">
            <h3>Cart Total</h3>
            <table>
                <tr>
                    <td>Cart Subtotal</td>
                    <td id="subtot" runat="server"></td>
                </tr>
                <tr>
                    <td>Delivery Fee</td>
                    <td>$ 4.00</td>
                </tr>
                <tr>
                    <td><strong>Total</strong></td>
                    <td><strong id="tot" runat="server"></strong></td>
                </tr>
            </table>
            <asp:Button ID="btn_ProceedToCheckout" class="btn btn-success" runat="server" Text="Place Order" />
        </div>
    </section>
    <script>
        var stripe = Stripe("pk_test_51MJo2iJ3Lmxbx4EB5Ea6rfbc9KfJl4DxHVUwupXYyd9BSfFQSHduxVeR4c8ynTCbb2qUlNPKZ2AnyWS8EjQJ2Eia00YMfwXfFh"); //Put Publishable key
        var form = document.getElementById("form1");
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            stripe.redirectToCheckout({
                sessionId: "<%= sessionId %>"
            })
        })
    </script>
</asp:Content>
