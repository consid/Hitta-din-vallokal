<?php include_once('header.php') ?>

<!-- Tillbaka -->
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="row">
            <a href=".">

                <div id="button-data"></div>
                <script id="button-template" type="text/x-handlebars-template">
                    <button type="button" class="btn btn-primary btn-header icon-arrow-left-light u-icon-left"><span>{{button}}</span></button>
                </script>

            </a>
        </div>
    </div>
</div>

<!-- Valdagen -->
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="row">
            <div id="fortidsrosta-data"></div>

            <script id="fortidsrosta-template" type="text/x-handlebars-template">
                <div class="jumbotron col-xs-12">
                    <h1>{{title}}</h1>
                    <p>
                        {{ingress}}
                    </p><br>
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">

                                <a href="#" style="cursor: pointer" class="btn btn-clean icon icon-information-norrmalm u-icon-right remove-title" tabindex="0" role="button" data-toggle="popover" data-placement="top" data-trigger="manual" data-content="{{fortidsrostaInfo}}" data-original-title="" data-html="true" data-title="<button type='button' id='close' class='close' onclick='closePopover()'>&times;</button>">
                                    <label>{{label}}</label>
                                </a>
                                <div class="visuallyhidden" id="autocompleteHelp">{{autocompleteHelp}}</div>

                                <!-- Sök -->
                                <!-- <div class="search-input">
                                    <input type="text" id="searchAdress" class="form-control autocomplete" placeholder="{{placeholder}}" data-list="searchAdress" data-no-match="Inga träffar" aria-describedby="autocompleteHelp" autocomplete="off">
                                    <div class="icon-search block-icon"></div>
                                </div> -->
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Google maps -->
                <div id="map"></div>
                <div id="mapinfo">
                    <div class="jumbotron results col-xs-12" id="results">
                        <div class="form-group">
                            <!-- <label for="vallokal">{{vallokalTitle}}</label> -->
                            <h2><div class="vallokal"></div><div class="valadress"></div><div class="valort"></div></h2>
                            <a id="latlng" href="" target="_blank" class="icon icon-find-on-map u-icon-left valoppettider">{{vallokalGoogleMaps}}</a>
                        </div>
                        <div class="form-group">
                            <label class="block" for="vallokal">{{vallokalstiderTitle}}</label>
                            <div class="inline">{{valoppettext}}</div> <div class="vallokalstiderIdag inline-desktop"></div> <div class="line-space">|</div>

                            <div class="toggle-list inline">
                                <a tabindex="0" class="btn icon icon-information-vasastan u-icon-right valoppettider" role="button">{{vallokalstiderFlerTider}}<div class="caret caret--special"></div></a>
                            </div>
                            <div class="oppet-list"></div>
                        </div>
                    </div>
                </div>
            </script>
            <script type="text/javascript" src="assets/js/data/googlemaps-fortidsrosta-get.js?RostaVersionString"></script>

        </div>
    </div>
</div>


<?php include_once('footer.php') ?>

<script type="text/javascript" src="assets/js/data/fortidsrosta.js?RostaVersionString"></script>
<script type="text/javascript" src="assets/js/data/previous.js?RostaVersionString"></script>
<script type="text/javascript">
    $('.toggle-list').click(function(e) {
        e.preventDefault();
        $('.toggle-list a').toggleClass('open');
        $('.oppet-list').slideToggle();
    });
</script>
