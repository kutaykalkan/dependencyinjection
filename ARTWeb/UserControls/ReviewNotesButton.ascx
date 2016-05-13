<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReviewNotesButton.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.ReviewNotesButton" %>
<webControls:ExImageButton ID="imgbtnDocument" runat="server" SkinID="ShowCommentPopup"
OnClientClick="return  openReviewNotesWindow(this)" AlternateText="Review Notes" />
