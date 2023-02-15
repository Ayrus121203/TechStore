<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralLayout.Master" AutoEventWireup="true" CodeBehind="UserOrderDetail.aspx.cs" Inherits="TechStore.UserOrderDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="page-header" class="about-header">
    </section>

    <section class="section-p1">
        <div class="card">
          <div class="card-body">
            <div class="container mb-5 mt-3">
              <div class="row d-flex align-items-baseline">
                <div class="col-xl-9">
                  <p style="color: #7e8d9f;font-size: 20px;">Invoice >> <strong>ID:<span id="invoice_top" runat="server"></span></strong></p>
                </div>
                <div class="col-xl-3 float-end">
                </div>
                <hr>
              </div>

              <div class="container">
                <div class="col-md-12">
                  <div class="text-center">
                    <img src="Images/logo.png" />
                    <p class="pt-0">Tech Store</p>
                  </div>
                </div>

                <div class="row">
                  <div class="col-xl-8">
                    <ul class="list-unstyled">
                      <li class="text-muted">To: <span style="color:#5d9fc5;" runat="server" id="name"></span></li>
                      <li class="text-muted" runat="server" id="billaddress"></li>
                      <li class="text-muted" runat="server" id="phonenum"><i class="fas fa-phone"></i></li>
                    </ul>
                  </div>
                  <div class="col-xl-4">
                    <p class="text-muted">Invoice</p>
                    <ul class="list-unstyled">
                      <li class="text-muted"><i class="fas fa-circle" style="color:#84B0CA ;"></i> <span
                          class="fw-bold">ID:</span>#<span id="invoice" runat="server"></span></li>
                      <li class="text-muted"><i class="fas fa-circle" style="color:#84B0CA ;"></i> <span
                          class="fw-bold" runat="server" id="orderdate">Order Date: </span></li>
                      <li class="text-muted"><i class="fas fa-circle" style="color:#84B0CA ;"></i> <span
                          class="me-1 fw-bold">Status:</span><span class="badge bg-warning text-black fw-bold">
                          PAID</span></li>
                    </ul>
                  </div>
                </div>

                <div class="row my-2 mx-1 justify-content-center">
                  <table class="table table-striped table-borderless">
                    <thead style="background-color:#84B0CA ;" class="text-white">
                      <tr>
                        <th scope="col">#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Qty</th>
                        <th scope="col">Unit Price</th>
                        <th scope="col">Amount</th>
                      </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptrOrderProd" runat="server">
                            <ItemTemplate>
                                  <tr>
                                    <th scope="row"><%#Eval("ProductID") %></th>
                                    <td><%#Eval("ProdName") %></td>
                                    <td><%#Eval("OrderedQuantity") %></td>
                                    <td>$<%#Eval("ProdSellPrice") %></td>
                                    <td>$<%#Eval("CartProdPrice") %></td>
                                  </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                  </table>
                </div>
                <div class="row">
                  <div class="col-xl-8">
                    <p class="ms-3">Add additional notes and payment information</p>

                  </div>
                  <div class="col-xl-3">
                    <ul class="list-unstyled">
                      <li class="text-muted ms-3"><span class="text-black me-4">SubTotal</span><span class="text-muted ms-3" runat="server" id="subtot"></span></li>
                      <li class="text-muted ms-3 mt-2"><span class="text-black me-4">Delivery Fee</span>$4</li>
                    </ul>
                    <p class="text-black float-start"><span class="text-black me-3"> Total Amount</span><span
                        style="font-size: 25px;" runat="server" id="tot"></span></p>
                  </div>
                </div>
                <hr>
                <div class="row">
                  <div class="col-xl-10">
                    <p>Thank you for your purchase</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    </section>
</asp:Content>
