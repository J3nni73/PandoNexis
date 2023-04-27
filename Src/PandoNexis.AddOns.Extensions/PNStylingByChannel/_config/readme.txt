Om man vill ha separata CSS-filer så krävs att man gör förändringar i webpack.es6.js

Filen ligger här i projektet:
	\Src\Litium.Accelerator.Mvc\Client\webpack\webpack.es6.js

Lägg till den/de CSS-filer som krävs genom att fylla på med Arrays under entry.

Exempel:

	siteclean: [
            path.resolve(CSS_DIR, 'siteclean.scss'), 
    ],

* Namnet på Arrayen kommer att styra dess namn. Mao, arrayen med namn siteclean kommer att skapa upp en fil som heter min.siteclean.css
* Dess Path pekar på den fil som den kommer att läsa ifrån. Så lägg den jämte Acceleratorns site.scss