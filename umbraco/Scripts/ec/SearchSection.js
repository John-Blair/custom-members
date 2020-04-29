$(function () {

    // Align front end and backend search results.
    const MaxSearchItems = 10;

    // Remote.
    var bloodhoundStates = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        // id, name, url, image
        remote: {
            url: '/Umbraco/Api/TaxonomySearch/Search?maxSearchItems=' + MaxSearchItems + '&q=%QUERY',
            wildcard: '%QUERY'
        }
    });


    // Remote search from content pages index.
    $('input.search').typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'products',
        displayKey: "Name",
        limit: MaxSearchItems,
        source: bloodhoundStates,
        templates:
        {
           suggestion: function(data)
           {
               var html = '<div class="suggestion">' +
                             '<a href="' + data.Url + '" data-name="'+ data.Name+ '">'  + 
                                '<img src="' + data.Image + '" />' +
                                '<div class="name-description">' +
                                   '<div class="name">' + data.Name + '</div>' +
                                   '<div class="description">' + data.Description + '</div></div></a ></div > ';

               //console.log(data);
               //console.log(html);
               return html;
           }
        }
    });
    
    $('input.search.tt-input').on("click", function (event) {
        //console.log("click input.search.tt-input");
        //console.log("input click: val is :" + $(this).val());
    });

    // Handle return hit in search bar.
    // and click on search icon.
    $('input.search.tt-input').on("keyup", function (event) {
        if (event.keyCode === 13) {
            //console.log("Enter detected");
            $("button.search").click();
        }
    });

    $('input.search.tt-input').focus();

    $('button.search').on("click", function (event) {

        // Search the suggestions and click a match or partial match otherwise goto the catalog.
        var searchTerm = $('input.search.tt-input').val().trim();
        //console.log("button.search click: with search term:" + searchTerm);

        // Search the suggestions
        // Exact match?
        var match$ =$('.suggestion a[data-name="' + searchTerm + '"]');
        if (match$.length > 0) {
            // Found an exact match. 
            console.log("Exact match on :" + searchTerm);
            // Click link to product.
            location.href = match$.first().attr('href');
        }
        else {
            // Try a partial match - any suggestion is a partial so pick the first.
            match$ =$('.suggestion a');
            if (match$.length > 0) {
                console.log("Partial match on " + searchTerm + "with product name" + match$.first().attr('data-name'));
                // Click link to product.
                location.href = match$.first().attr('href');
            }
            else {
                // Do nothing.
                //console.log("Goto catalog: No match on " + searchTerm);
                //location.href = AllProductsUrl;
            }
             
        }
    });

});