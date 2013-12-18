    function MoveItem(ctrlSource, ctrlTarget) {
        var Source = document.getElementById(ctrlSource);
        var Target = document.getElementById(ctrlTarget);

        if ((Source != null) && (Target != null)) {
            while ( Source.options.selectedIndex >= 0 ) {
                var newOption = new Option(); // Create a new instance of ListItem
                newOption.text = Source.options[Source.options.selectedIndex].text;
                newOption.value = Source.options[Source.options.selectedIndex].value;
               
                Target.options[Target.length] = newOption; //Append the item in Target
                Source.remove(Source.options.selectedIndex);  //Remove the item from Source
            }
        }
    }
