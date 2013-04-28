Java Access Bridge .Net Sample
==============================

This is a really basic sample application on how to use the Java Access Bridge in .Net.  It's a VS2010 project that targets .Net 2. It's able to read the entire control hierarchy of a java app and dump it to screen.  For my purposes that's all I needed.  If you're just starting out with the JAB on .Net this should be a good head start for whatever you need to do.

### To use the sample app: ###

* Install Java and the Java Access Bridge
* Open and run the sample solution in VS2010 or higher
* Open Java app of interest
* Use Spy++ or some other tool to get the window handle to the java window (in hex format)
* Enter the windle handle into the sample app and click "Dump Control Hierarchy"

This is a slightly tweaked copy of some code I found in an [OTN forum post](https://forums.oracle.com/forums/thread.jspa?threadID=1291531&tstart=2&messageID=9651787#9651787).  A big thanks goes out to them. I just figured I'd make the code a bit more findable and easier to get started with. It adds getting the value of the control and some info of the control tree hierarchy to make it easier to find controls of interest. 
