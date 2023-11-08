# Mabinogi Trade Tracker

A tool for tracking the current availability of trade ships in Mabinogi.
It's called Mabinogi Trade Tracker because I intended to do more with it.
Requires you to adjust a server time offset that is approximately the time
at which the North America Mabinogi server goes online, which is more or
less 10 AM on Thursday every week.

## Features

* Six timers tell you when the ships are boarding or not.

## Screenshots

![Screenshot 1](/screenshot_1.png "Screenshot of the main window")

## Using The Tool

On any given week from one maintenence to the next, you must check the
latest scheduled maintenence notice on the Mabinogi website to get a time
when the maintenance has been completed. You can do that by going to the
website and hitting the Read More News button. Use the Filter By dropdown
list and select Maintenence, then select the latest maintenence post.
The time that the maintenence was completed is shown at the top.

When you use Mabinogi Trade Tracker for the first time this week, you'll
need to adjust the server time offset to be near the maintenence completion
time. Then you go to any of the ships that travel Uladh-Iria. Attempt to
board the ship. Adjust the server time in Mabinogi Trade Tracker to match
the time in the game. The timer should be green while you have boarded and
are waiting for departure, red when you have to wait to board the ship.

After the Uladh-Iria ship is correct, check one of the ships that travel
Cobh-Belvast. If the Cobh-Belvast ship time is wrong, add or subtract 11
minutes. If you are unsure which direction to go, just try both. The
correct direction is closer to and earlier than the time maintenence
was completed.

When you close Mabinogi Trade Tracker, the position of the window and the
server time offset are saved to a configuration text file.

## License

This software is free to use, copy, modify, merge, and distribute.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
IN THE SOFTWARE.