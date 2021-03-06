﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/MaxiPoe.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pipe9 Feedback - Feedback
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Styles/themes/default.min.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/ref/Feedback/jquery.sceditor.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.10.0/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Scripts/ref/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/ref/Feedback/Resumefeedback.js"></script>
    <script type="text/javascript" src="../../Scripts/ref/Namespace.js"></script>
    <script type="text/javascript" src="../../Scripts/ref/Requests.js"></script>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function DropDown(el) {
            this.dd = el;
            this.initEvents();
        }
        DropDown.prototype = {
            initEvents: function () {
                var obj = this;

                obj.dd.on('click', function (event) {
                    $(this).toggleClass('active');
                    event.stopPropagation();
                });
            }
        }

        $(function () {

            var dd = new DropDown($('#dd'));

            $(document).click(function () {
                // all dropdowns
                $('.wrapper-dropdown-2').removeClass('active');
            });

        });
    </script>
    <div class="slideout">
        <div class="slidecontent">
            <div id="ajax-content">
            </div>
            <div id="content1">
                <h2>
                    Feedback Introduction</h2>
                <div class="content1" id="poeintro">
                </div>
            </div>
            <div id="content2">
                <h2>
                    Module intro</h2>
                <div class="content2" id="moduleintro">
                </div>
            </div>
            <div id="content3">
                <h2>
                    Traits</h2>
                <div class="content3" id="poetraits">
                    
                </div>
            </div>
            <div id="content4">
                <h2>
                    Key actions</h2>
                <div class="content4" id="modulekeyaction">
                </div>
            </div>
            <div id="primarynav">
                <ul id="navigation">
                    <li><a href="#" class="selected" rel="content1">
                        <img src="../../Images/navigation1.png" alt="Nav1" /></a></li>
                    <li><a href="#" rel="content2">
                        <img src="../../Images/navigation2.png" alt="Nav2" /></a></li>
                    <li><a href="#" rel="content3">
                        <img src="../../Images/navigation3.png" alt="Nav3" /></a></li>
                    <li><a href="#" rel="content4">
                        <img src="../../Images/navigation4.png" alt="Nav4" /></a></li>
                </ul>
            </div>
        </div>
        <div id="left">
        </div>
    </div>
    <div class="slideout1">
        <div class="slidecontent1">
            <h1>
                Introduction</h1>
            <ul>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <div class="up-down">
                        <div class="slide default">
                            <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></div>
                        <div class="slide onhover">
                            <%--Jackson Robert--%></div>
                    </div>
                </a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
                <li><a href="#">
                    <img src="../../Images/flower.jpg" width="95" height="95" alt="Intro" /></a></li>
            </ul>
            <div class="clr">
            </div>
            <form action="" method="post">
            <div>
                <textarea name="bbcode_field" style="height: 170px; width: 295px;" id="txtareas">
</textarea>
            </div>
            <div class="clr">
            </div>
            <input type="button" id="btn" value="send" />
            </form>
        </div>
        <div id="right">
        </div>
    </div>
    <div class="pageholder">
        <ul>
            <li>
                <div>
                    <div>
                        <div id="tiles">
                            <div>
                                <div class="breadcrumb">
                                    Home > Feedback > Take Feedback</div>
                                <input type="hidden" id="modulequestions" />
                                <input type="hidden" id="moduleid" />
                                <input type="hidden" id="currentQuestionorder" value="0" />
                                <img id="prevdv" class="prevbtn" src="../../Images/prev_arrow.png" onclick="Javascript:details_feedback.loadPrevContent();" />
                                <h1>
                                    Take Self Feedback</h1>
                                <div class="quickview">
                                    <div id="poemodule">
                                        
                                    </div>
                                </div>
                                <div>
                                    <input type="hidden" id="totalQuestions" />
                                    <input type="hidden" id="currentQuestionno" value="1" />
                                    <input type="hidden" id="totalmodules" />
                                    <input type="hidden" id="currentmoduleOrder" />
                                    <input type="hidden" id="module1" />
                                    <input type="hidden" id="module2" />
                                    <input type="hidden" id="module3" />
                                    <input type="hidden" id="module4" />
                                    <input type="hidden" id="module5" />
                                    <input type="hidden" id="module6" />
                                    <input type="hidden" id="module7" />
                                    <input type="hidden" id="moduleintrohdn" />
                                    <input type="hidden" id="poeintrohdn" />
                                    <input type="hidden" id="poetraitshdn" />
                                    <input type="hidden" id="modulekeyactionhdn" />
                                    <input type="hidden" id="lastSavedQuestion" />
                                    <input type="hidden" id="lastSavedQuestionOrder" />
                                    <input type="hidden" id="lastSavedModuleId" />
                                    <input type="hidden" id="lastSavedModuleOrder" />
                                </div>
                                <div class="contentholder" style="margin-top: -16px">
                                    <div class="feedback_question">
                                        <div class="qndiv">
                                            <div id="compquestionid">
                                                <%-- Question 1/18--%>
                                            </div>
                                            <div>
                                                <div style="float: left;">
                                                    | Time taken :
                                                </div>
                                                <div id="counterHour">
                                                    0</div>
                                                <div id="counterMin">
                                                    0</div>
                                                <div style="float: left;">
                                                    &nbsp; : &nbsp;</div>
                                                <div id="counterSec">
                                                    00</div>
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                        <div class="TsfQno" id="Qid">
                                            1
                                        </div>
                                        <h2 id="Qtext">
                                        </h2>
                                        <div class="clr">
                                        </div>
                                        <div id='Qno1' style="width: 562px; height: 62px; cursor: pointer;">
                                            <img src="../../Images/img/slidebg1.png" />
                                        </div>
                                        <h3>
                                            Frequency Definitions</h3>
                                        <div class="feedbackright">
                                            <h4>
                                                Rarely</h4>
                                            <p>
                                                The Excellence Action is seldom observed or produced as a measure of success
                                                (Less than 20% of the time)</p>
                                            <h4>
                                                In-Consistently</h4>
                                            <p>
                                                The Excellence Action is only observed or produced some of the time (less than
                                                60% of the time)</p>
                                            <h4>
                                                Frequently</h4>
                                            <p>
                                                The Excellence Action is consistently observed or produced (less than 90% of
                                                the time)</p>
                                            <h4>
                                                Always</h4>
                                            <p>
                                                The Excellence Action is always observed or produced as a measure of success</p>
                                        </div>
                                        <div class="feedbackright01" id="questionindetail">
                                            <h4>
                                                In Detail</h4>
                                            <p>
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                    </div>
                                </div>
                                <input type="hidden" id="question1" />
                                <input type="hidden" id="selectedanswer" value="1" />
                                <img id="nextdv" class="nextbtn" src="../images/next_arrow.png" />
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
        <div class="wrapper-demo">
            <div id="dd" class="wrapper-dropdown-2">
                Practice Area
                <ul class="dropdownlist">
                </ul>
            </div>
        </div>
    </div>
</asp:Content>