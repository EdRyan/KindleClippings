KindleClippings
===============

.NET parser for the Amazon Kindle's "My Clippings.txt" file

How To Use
----------

1. Build the solution
2. Copy "My Clippings.txt" from your Kindle (e.g. by connecting it to your PC as a USB drive) to the output directory containing "KindleClippings.exe"
3. Run in debug mode and breakpoint to see the clippings... (temporary)

TODO
----

1. Move parser classes to their own class library project
2. Group clippings by book
3. Some kind of GUI project to view the data
	a. Stop hardcoding the "My Clippings.txt" path
	b. Lose the Console project
	c. Calibre integration?
4. Document the library
5. Some kind of installer and publishing mechanism