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

- GUI: Actually view the highlights/notes
- GUI: Icons
- Calibre integration?
- Document the library
- Some kind of installer and publishing mechanism
- Test suite
- Pull data directly from Kindle (i.e. eliminate manual txt file copying)
	- Identify connected Kindle, find "My Clippings.txt" and proceed as usual
	- Reading MBS files???