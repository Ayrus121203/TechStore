<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDefault.aspx.cs" Inherits="TechStore.AdminDefault" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section style="background-color:aliceblue">
    <div class="container-fluid">
        <div class="values">
            <asp:Repeater ID="rptrTotalUsers" runat="server">
                <ItemTemplate>
                    <div class="val-box">
                        <i class="fas fa-users"></i>
                        <div>
                            <h3><%#Eval("UserCount") %></h3>
                            <span>Total Users</span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptrTotOrders" runat="server">
                <ItemTemplate>
                    <div class="val-box">
                        <i class="fas fa-shopping-cart"></i>
                        <div>
                            <h3><%#Eval("OrderCount") %></h3>
                            <span>Total Orders</span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptrProductsSold" runat="server">
                <ItemTemplate>
                <div class="val-box">
                    <i class="fa-solid fa-right-left"></i>
                    <div>
                        <h3><%#Eval("ProdsSold") %></h3>
                        <span>Products Sold</span>
                    </div>
                </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptrTtProfits" runat="server">
                <ItemTemplate>
                    <div class="val-box">
                        <i class="fa-solid fa-sack-dollar"></i>
                        <div>
                            <h3>$<%#Eval("Profits") %></h3>
                            <span>Profits</span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <section class="section-p1">
    <div class="row">
        <div class="col-xl-6 col-lg-6">
            <!-- Bar Chart -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Product Ratings</h6>
                </div>
                <div class="card-body">
                    <div class="chart-bar">
                        <canvas id="myBarChart"></canvas>
                    </div>
                    <hr>
                    Styling for the bar chart can be found in the
                   
                    <code>/js/demo/chart-bar-demo.js</code> file.
               
                </div>
            </div>views
        </div>
        <div class="col-xl-6 col-lg-6">
            <!-- Bar Chart -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Website Ratings</h6>
                </div>
                <div class="card-body">
                    <div class="chart-bar">
                        <canvas id="websiterating"></canvas>
                    </div>
                    <hr>
                    Styling for the bar chart can be found in the
                   
                    <code>/js/demo/chart-bar-demo.js</code> file.
               
                </div>
            </div>
        </div>
    </div>
        </section>



    </div>
</section>
    <script>

        // Set new default font family and font color to mimic Bootstrap's default styling
        Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
        Chart.defaults.global.defaultFontColor = '#858796';

        function number_format(number, decimals, dec_point, thousands_sep) {
            // *     example: number_format(1234.56, 2, ',', ' ');
            // *     return: '1 234,56'
            number = (number + '').replace(',', '').replace(' ', '');
            var n = !isFinite(+number) ? 0 : +number,
                prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
                sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
                dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
                s = '',
                toFixedFix = function (n, prec) {
                    var k = Math.pow(10, prec);
                    return '' + Math.round(n * k) / k;
                };
            // Fix for IE parseFloat(0.55).toFixed(0) = 0;
            s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
            if (s[0].length > 3) {
                s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
            }
            if ((s[1] || '').length < prec) {
                s[1] = s[1] || '';
                s[1] += new Array(prec - s[1].length + 1).join('0');
            }
            return s.join(dec);
        }

        var chartLabels;
        var chartData;

        </script>

    <asp:Literal ID="ltChartData" runat="server" />

    <script>

        // Bar Chart Example
        var ctx = document.getElementById("myBarChart");
        var myBarChart = new Chart(ctx, {
            type: 'horizontalBar',
            data: {
                labels: chartLabels,
                datasets: [{
                    label: "Star",
                    backgroundColor: "#4e73df",
                    hoverBackgroundColor: "#2e59d9",
                    borderColor: "#4e73df",
                    data: chartData,
                }],
            },
            options: {
                
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        left: 10,
                        right: 25,
                        top: 25,
                        bottom: 0
                    }
                },
                indexAxis: 'x',
                scales: {
                    xAxes: [{
                        label: "Name",
                        gridLines: {
                            display: false,
                            drawBorder: false
                        },
                        ticks: {
                            maxTicksLimit: 8
                        },
                        maxBarThickness: 25,
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 5,
                            maxTicksLimit: 5,
                            padding: 10,
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return '' + number_format(value);
                            }
                        },
                        gridLines: {
                            color: "rgb(234, 236, 244)",
                            zeroLineColor: "rgb(234, 236, 244)",
                            drawBorder: false,
                            borderDash: [2],
                            zeroLineBorderDash: [2]
                        }
                    }],
                },
                legend: {
                    display: true
                },
                tooltips: {
                    titleMarginBottom: 10,
                    titleFontColor: '#6e707e',
                    titleFontSize: 14,
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    caretPadding: 10,
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': ' + number_format(tooltipItem.yLabel);
                        }
                    }
                },
            }
        });

    </script>
    <asp:Literal ID="ltWebsiteRating" runat="server" />
    <script>

        // Bar Chart Example
        var ctx = document.getElementById("websiterating");
        var myBarChart = new Chart(ctx, {
            type: 'horizontalBar',
            data: {
                labels: chartLabels,
                datasets: [{
                    label: "Star",
                    backgroundColor: "#4e73df",
                    hoverBackgroundColor: "#2e59d9",
                    borderColor: "#4e73df",
                    data: chartData,
                }],
            },
            options: {

                maintainAspectRatio: false,
                layout: {
                    padding: {
                        left: 10,
                        right: 25,
                        top: 25,
                        bottom: 0
                    }
                },
                indexAxis: 'x',
                scales: {
                    xAxes: [{
                        label: "Name",
                        gridLines: {
                            display: false,
                            drawBorder: false
                        },
                        ticks: {
                            min: 0,
                            max: 5,
                            maxTicksLimit: 5,
                        },
                        maxBarThickness: 25,
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: 5,
                            maxTicksLimit: 5,
                            padding: 10,
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return '' + number_format(value);
                            }
                        },
                        gridLines: {
                            color: "rgb(234, 236, 244)",
                            zeroLineColor: "rgb(234, 236, 244)",
                            drawBorder: false,
                            borderDash: [2],
                            zeroLineBorderDash: [2]
                        }
                    }],
                },
                legend: {
                    display: true
                },
                tooltips: {
                    titleMarginBottom: 10,
                    titleFontColor: '#6e707e',
                    titleFontSize: 14,
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    caretPadding: 10,
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': ' + number_format(tooltipItem.yLabel);
                        }
                    }
                },
            }
        });

    </script>
</asp:Content>
