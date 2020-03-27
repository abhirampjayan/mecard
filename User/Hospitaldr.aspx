<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="Hospitaldr.aspx.cs" Inherits="Hospital_Hospital" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="style.css" rel="stylesheet" />
    <script src="jquery.scrollTo.min.js"></script>
    <script src="jquery.serialScroll.js"></script>
    <%--<script src="onlineBookingScroller.js"></script>--%>


    <script type="text/javascript">
        // Easing equation, borrowed from jQuery easing plugin
        // http://gsgd.co.uk/sandbox/jquery/easing/
        jQuery.easing.easeOutQuart = function (x, t, b, c, d) {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        };

        jQuery(function ($) {
            /**
			 * Most jQuery.serialScroll's settings, actually belong to jQuery.ScrollTo, check it's demo for an example of each option.
			 * @see http://flesler.demos.com/jquery/scrollTo/
			 * You can use EVERY single setting of jQuery.ScrollTo, in the settings hash you send to jQuery.serialScroll.
			 */

            /**
			 * The plugin binds 6 events to the container to allow external manipulation.
			 * prev, next, goto, start, stop and notify
			 * You use them like this: $(your_container).trigger('next'), $(your_container).trigger('goto', [5]) (0-based index).
			 * If for some odd reason, the element already has any of these events bound, trigger it with the namespace.
			 */

            /**
			 * IMPORTANT: this call to the plugin specifies ALL the settings (plus some of jQuery.ScrollTo)
			 * This is done so you can see them. You DON'T need to specify the commented ones.
			 * A 'target' is specified, that means that #screen is the context for target, prev, next and navigation.
			 */
            $('#screen').serialScroll({
                target: '#sections',
                items: 'li', // Selector to the items ( relative to the matched elements, '#sections' in this case )
                prev: 'img.prev',// Selector to the 'prev' button (absolute!, meaning it's relative to the document)
                next: 'img.next',// Selector to the 'next' button (absolute too)
                axis: 'xy',// The default is 'y' scroll on both ways
                navigation: '#navigation li a',
                duration: 700,// Length of the animation (if you scroll 2 axes and use queue, then each axis take half this time)
                force: true, // Force a scroll to the element specified by 'start' (some browsers don't reset on refreshes)

                //queue:false,// We scroll on both axes, scroll both at the same time.
                //event:'click',// On which event to react (click is the default, you probably won't need to specify it)
                //stop:false,// Each click will stop any previous animations of the target. (false by default)
                //lock:true, // Ignore events if already animating (true by default)		
                //start: 0, // On which element (index) to begin ( 0 is the default, redundant in this case )		
                //cycle:true,// Cycle endlessly ( constant velocity, true is the default )
                //step:1, // How many items to scroll each time ( 1 is the default, no need to specify )
                //jump:false, // If true, items become clickable (or w/e 'event' is, and when activated, the pane scrolls to them)
                //lazy:false,// (default) if true, the plugin looks for the items on each event(allows AJAX or JS content, or reordering)
                //interval:1000, // It's the number of milliseconds to automatically go to the next
                //constant:true, // constant speed

                onBefore: function (e, elem, $pane, $items, pos) {
                    /**
					 * 'this' is the triggered element 
					 * e is the event object
					 * elem is the element we'll be scrolling to
					 * $pane is the element being scrolled
					 * $items is the items collection at this moment
					 * pos is the position of elem in the collection
					 * if it returns false, the event will be ignored
					 */
                    //those arguments with a $ are jqueryfied, elem isn't.
                    e.preventDefault();
                    if (this.blur) {
                        this.blur();
                    }
                },
                onAfter: function (elem) {
                    //'this' is the element being scrolled ($pane) not jqueryfied
                }
            });

            /**
			 * No need to have only one element in view, you can use it for slideshows or similar.
			 * In this case, clicking the images, scrolls to them.
			 * No target in this case, so the selectors are absolute.
			 */

            $('#slideshow').serialScroll({
                items: 'li',
                prev: '#screen2 a.prev',
                next: '#screen2 a.next',
                offset: -230, //when scrolling to photo, stop 230 before reaching it (from the left)
                start: 1, //as we are centering it, start at the 2nd
                duration: 1200,
                force: true,
                stop: true,
                lock: false,
                cycle: true, //don't pull back once you reach the end
                easing: 'easeOutQuart', //use this easing equation for a funny effect
                //jump: true //click on the images to scroll to them
            });

            ////////////////////////////////////////////////////////////////////////////
            $('#slideshow1').serialScroll({
                items: 'li',
                prev1: '#screen3 a.prev1',
                next1: '#screen3 a.next1',
                offset: -230, //when scrolling to photo, stop 230 before reaching it (from the left)
                start: 1, //as we are centering it, start at the 2nd
                duration: 1200,
                force: true,
                stop: true,
                lock: false,
                cycle: false, //don't pull back once you reach the end
                easing: 'easeOutQuart', //use this easing equation for a funny effect
                jump: true //click on the images to scroll to them
            });


            /**
			 * The call below, is just to show that you are not restricted to prev/next buttons
			 * In this case, the plugin will react to a custom event on the container
			 * You can trigger the event from the outside.
			 */

            var $news = $('#news-ticker');//we'll re use it a lot, so better save it to a var.
            $news.serialScroll({
                items: 'div',
                duration: 2000,
                force: true,
                axis: 'y',
                easing: 'linear',
                lazy: true,// NOTE: it's set to true, meaning you can add/remove/reorder items and the changes are taken into account.
                interval: 1, // yeah! I now added auto-scrolling
                step: 2 // scroll 2 news each time
            });

            /**
			 * The following you don't need to see, is just for the "Add 2 Items" and "Shuffle"" buttons
			 * These exemplify the use of the option 'lazy'.
			 */
            $('#add-news').click(function () {
                var
					$items = $news.find('div'),
					num = $items.length + 1;

                $items.slice(-2).clone().find('h4').each(function (i) {
                    $(this).text('News ' + (num + i));
                }).end().appendTo($news);
            });
            $('#shuffle-news').click(function () {//don't shuffle the first, don't wanna deal with css
                var shuffled = $news.find('div').get().slice(1).sort(function () {
                    return Math.round(Math.random()) - 0.5;//just a random number between -0.5 and 0.5
                });
                $(shuffled).appendTo($news);//add them all reordered
            });
        });
    </script>

    <link href="../User/css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="../User/js/jquery.min.js"></script>

    <link href="../User/css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="../User/js/jquery.bxslider.js"></script>
    <script type="text/javascript">
        $('.bxslider').bxSlider({
            mode: 'fade',
            captions: true,
            pager: false,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    </script>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
        type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function CheckParts() {
            __doPostBack('', '');
        };
        $(document).ready(function () {
            $("#<%=TextBox1.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/Service.asmx/GetCustomers1") %>',
                    data: "{ 'prefix': '" + request.term + "'}",

                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
                $("#<%=TextBox1.ClientID %>").val(i.item.label);
                CheckParts();
            },
            minLength: 1
        });




    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">

        <!-- Info boxes -->
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="box box-solid">
                    <div class="box-header">
                        <h3 class="box-title pull-left">Available doctors</h3>
                        <div class="pull-right col-md-6">
                        <div class="col-md-4">
                            <asp:TextBox ID="TextBox1" CssClass="form-control btn-xs" placeholder="Doctor name" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="TextBox2" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TextBox2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="Button4" CssClass="btn btn-success" runat="server" Text="Search" OnClick="Button1_Click" />
                        </div>

                            </div>
                    </div>

                </div>
            </div>
            <!-- /.col -->

        </div>
        <%--<div class="row">--%>
        <div class="col-md-4 col-lg-4">
        </div>
        <div class="col-md-4 col-lg-4">
        </div>
        <div class="col-md-4 col-lg-4">

            <!-- /.row -->

            <asp:HiddenField ID="hfCustomerId" runat="server" />
        </div>
        <div>
            <asp:Button ID="btnForAjax" runat="server" Text="" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup">
                <%--<div class="col-md-5">
                                    <div class="modal modal-primary">
                                        <div class="modal-header">helloo</div>
                                        <div class="modal-body">--%>
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title">Make Appointment</h3>
                            <div class="box-tools pull-right">
                                <%--<button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <%--<asp:DataList ID="DataList1" CssClass="table" runat="server" RepeatColumns="5"></asp:DataList>--%>

                                <div class="form-group">
                                    <label>Date</label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="bb" CssClass="form-control" runat="server" ReadOnly="true" placeholder="Appointment date"></asp:TextBox>
                                    <%--<span class="input-group-btn">
                                        <asp:Button ID="Button2" ValidationGroup="bb" CssClass="btn btn-success" runat="server" Text="Check availability" OnClick="Button1_Click" />
                                    </span>--%>
                                </div>
                                <div class="form-group">
                                    <label>Appointment time</label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Appointment Time"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Reason to visit</label>
                                    <%--<asp:TextBox ID="TxtReasonToVisit" runat="server" CssClass="form-control" Enabled="False" placeholder=" Reason"></asp:TextBox>--%>
                                    <asp:DropDownList ID="TxtReasonToVisit" runat="server" CssClass="form-control">
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <label>Payment option</label>
                                    <asp:DropDownList ID="DdlPayments" runat="server" CssClass="form-control">
                                        <asp:ListItem>Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Hakkeem user id</label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="b"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" placeholder="Hakkeem user id" OnTextChanged="TxtBookDocUserId_TextChanged"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="b" CssClass="btn btn-success pull-right" OnClick="BtnTakeAppointment_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <%-- </div>
                                    </div>
                                </div>--%>
                <%-- <asp:Label ID="Label1" runat="server" Text="Here is the message which will come!!!"></asp:Label><br />
                                <br />
                                <asp:Button ID="btnYes" runat="server" Text="Yes" />
                                <asp:Button ID="btnNo" runat="server" Text="NO" />--%>
            </asp:Panel>
            <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">Hakkeem</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button1_Click" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>

            <asp:Button ID="btnForAjax4" runat="server" Text="" Style="display: none" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup">

                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">Hakkeem</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose4" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button3_Click" Style="height: 25px" /></span>
                        </div>
                    </div>
                </div>


            </asp:Panel>

        </div>


    </section>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.4;
        }

        .modalPopup {
            /*background-color: #FFFFFF;*/
            border-width: 0px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 3px;
            width: 500px;
            height: 300px;
        }
    </style>

</asp:Content>

