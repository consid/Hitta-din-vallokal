<?php include_once('header.php') ?>

<!-- Startpage  -->
<div class="row">
	<div class="col-md-12 col-sm-12 col-xs-12">
		<div class="row">
	    	<div class="jumbotron" id="startpage-data"></div>
	   
	    	<script id="startpage-template" type="text/x-handlebars-template">
		        <!-- {{ingress}} -->
			    <h1 class="vallokal-title">{{title}}</h1>
		        <p>
		        	<a href="valdagen.php"><button type="button" class="btn btn-lg btn-primary">{{link1}}</button></a>
		        </p>
		        <p>
		        	<a href="fortidsrosta.php"><button type="button" class="btn btn-lg btn-primary">{{link2}}</button></a>
		        </p>
	   		</script>
	   		
		</div>
	</div>
</div>


<?php include_once('footer.php') ?>

<script>
	$.ajax({ url: "https://api-url-here.azurewebsites.net/api/sok" });
</script>
<script type="text/javascript" src="assets/js/data/index.js?RostaVersionString"></script>