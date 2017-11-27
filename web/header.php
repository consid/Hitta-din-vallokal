<!DOCTYPE html>
<html lang="sv">

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="https://static.stockholm.se/styleguide-etjanst/latest/favicon.ico">

    <title>Rösta i Stockholm - Stockholms stad</title>

    <!-- Custom styles for this template -->
    <link href="assets/css/themes/stockholm/style.css?RostaVersionString" rel="stylesheet" media="screen">
    <link href="assets/css/themes/stockholm/print.css?RostaVersionString" rel="stylesheet" media="print">
    <link href="assets/css/custom/import/style-custom.css?RostaVersionString" rel="stylesheet">

    <!-- FontAwesome Icons -->
    <link rel="stylesheet" href="assets/css/font-awesome/css/font-awesome.min.css?RostaVersionString">
    <link rel="stylesheet" href="assets/js/vendor/jquery-ui.min.css?RostaVersionString">

    <!-- Only for visual examples (like icons and buttons), remove this in production -->
    <link href="assets/css/themes/stockholm/docs.css?RostaVersionString" rel="stylesheet">
    <link href="assets/css/themes/stockholm/visual.css?RostaVersionString" rel="stylesheet">
    <link href="assets/css/themes/stockholm/noscript.css?RostaVersionString" rel="stylesheet" media="print">

    <link rel="stylesheet" href="//font.stockholm.se/css/stockholm-type.css?RostaVersionString" media="all">
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Open+Sans:400italic,400,300,600&subset=latin" media="all">
    <link rel="stylesheet" href="assets/icons/icons.data.svg.css?RostaVersionString" media="all">
</head>
<body>
	<div class="container" role="main">
		<div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="row">
                    <header role="banner" class="site-header container col-xs-12">
                        <a accesskey="1" title="Till startsidan" class="logotype" rel="home" href=".">
                            <img src="assets/img/logo_print.svg" alt="stockholm.se" width="185" height="63" class="logotype__st-erik-header">
                            <img src="assets/img/logo_no-title_print.svg" alt="stockholm.se" width="185" height="63" class="logotype_small__st-erik-header">
                            <div class="logotype__site-name-wrapper">
                                <div id="logotype-data"></div>
                                <script id="logotype-template" type="text/x-handlebars-template">
                                    <strong class="logotype__site-name">{{headerTitle}}</strong>
                                </script>

                                <span class="logotype__site-subtitle"></span>
                            </div>
                        </a>

                        <li class="dropdown">
                            <a id="dropdownMenuLang" href="#" class="dropdown-toggle icon-world-map-light" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><div class="icon-world-map block-icon"></div><span class="langText">Sv</span> <span class="caret caret--special"></span></a>
                            <ul class="dropdown-menu right" id="languageDropdown" aria-labelledby="dropdownMenuLang">
                                <li class="active sv" onClick="window.location.reload()" data-toggle="dropdown">Svenska</li>
                                <li class="en" onClick="window.location.reload()" data-toggle="dropdown">English</li>
                            </ul>
                        </li>
                    </header>
                </div>
            </div>
        </div>