KindleClippings
===============

.NET parser for the Amazon Kindle's "My Clippings.txt" file.

The solution has two components, a library (KindleClippings) and GUI (KindleClippingsGUI) which provides a sample front-end for the library.

The GUI uses [Eto.Forms](https://github.com/picoe/Eto) for cross-platform compatibility.

How To Use
----------

__Library__


1. Build the solution and reference the outputted KindleClippings.dll
2. Call MyClippingsParser.Parse() using either (a) the path to a "My Clippings.txt" file or (b) a stream matching the format of a "My Clippings.txt" file to return the clippings
3. Optionally call ClippingOrganizer.GroupClippingsByAuthorAndBook(), passing in the clippings, to organize them

__GUI__

1. Copy "My Clippings.txt" from your Kindle (e.g. by connecting it to your PC as a USB drive) to your computer
2. Build/Run the GUI
3. Browse to the location of the "My Clippings.txt" file
4. Click "Parse"
5. See everything but your highlights and notes in the tree view (this step is in progress)


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