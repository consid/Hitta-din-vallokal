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
            <div class="jumbotron col-xs-12">
                <div id="valdagen-data"></div>
                
                <script id="valdagen-template" type="text/x-handlebars-template">
                    <div class="alert alert-info fade in alert-dismissable" role="alert">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close" title="close">&times;</a>
                        {{info}}
                    </div>
                    <h1>{{title}}</h1>
                    <p>
                        {{ingress}}
                    </p><br>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6">
                            <div class="form-group" id="results">

                                <a href="#" class="btn btn-clean icon icon-information-norrmalm u-icon-right" tabindex="0" role="button" data-toggle="popover" data-placement="top" data-content="{{folkbokforingsadressinfo}}" data-original-title="" data-html="true" title="<button type='button' id='close' class='close' onclick='closePopover()'>&times;</button>">
                                    <label>{{label}}</label>
                                </a>
                                
                                <div class="search-input">
                                    <input type="text" id="searchtags" class="form-control autocomplete" placeholder="{{placeholder}}" data-list="searchtags" data-no-match="Inga träffar" aria-describedby="autocompleteHelp" autocomplete="off">
                                    <div class="icon-search block-icon"></div>
                                    <div class="empty-message">
                                        <div class="alert alert-info" role="alert">
                                            {{searchTitle}}
                                        </div>
                                        <p>
                                            {{searchText}}
                                        </p>
                                        <p>
                                            <a href="tel:{{telefonnummerLink}}"><i class="icon fa fa-phone"></i> {{telefonnummer}}</a> <!-- | 
                                            <a tabindex="0" class="icon icon-information-vasastan u-icon-right" role="button" data-placement="bottom" data-html="true" data-toggle="popover" data-trigger="focus" title="" data-content="{{#each oppettider}} <span>{{oppettid}}</span> {{/each}}" data-original-title="{{tooltipTitle}}">{{titleOppet}}</a> -->
                                        </p>
                                        <p>
                                            <a href="mailto:{{infoMail}}"><i class="icon fa fa-envelope"></i> {{infoMail}}</a>
                                        </p>
                                    </div>
                                </div>
                                <div class="visuallyhidden" id="autocompleteHelp">{{autocompleteHelp}}</div>
                            </div>
                        </div>
                    </div>
                </script>
            
            </div>

            <div id="vallokal-data"></div>
                
            <script id="vallokal-template" type="text/x-handlebars-template">
                <div class="results">
                    <div class="search-results">
                        <div class="jumbotron col-xs-12">

                            <div class="form-group">
                                <label for="vallokal">{{vallokalTitle}}</label>
                                <h2>{{vallokal}}<br>{{valadress}}<br>{{valort}}</h2>
                                <a id="latlng" href="https://maps.google.com/?daddr={{valadressGeo}},{{valort}},Sverige/{{valLat}},{{valLng}},15z" target="_blank" class="icon icon-find-on-map u-icon-left valoppettider">{{vallokalGoogleMaps}}</a>
                            </div>
                            <div class="form-group">
                                <label for="vallokal">{{valdistriktTitle}}</label>
                                <h2>{{valdistriktsnamn}}</h2>
                            </div>
                            <div class="form-group">
                                <label for="vallokal">{{vallokalstiderTitle}}</label>
                                <p>{{valdatum}}<br>{{valtid}}</p>
                            </div>
                        </div>
                    </div>

                    <!-- Google maps -->
                    <div id="map"></div>
                    <div id="lat" class="hidden">{{valLat}}</div>
                    <div id="lng" class="hidden">{{valLng}}</div>

                </div>

            </script>

       
        </div>
    </div>
</div>

<?php include_once('footer.php') ?>
  
<script type="text/javascript" src="assets/js/data/valdagen.js?RostaVersionString"></script>
<script type="text/javascript" src="assets/js/data/previous.js?RostaVersionString"></script>
