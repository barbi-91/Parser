# Parser
Console app Parser is useful for saving user story logs, and parsing tags on a specific date.

Console app Parser take specific text from given (added.txt) file: nolr-work-log-2023.txt.
The Parser goes through the lines of text and checks whether the beginning of the line is entered as a number in the first and second place, if there is a number in the first and second place, it adds the year to it, then checks whether a specific mark such as: EH, MH, WZ, IP,,FX3,MHRZ is entered after the date  and if it is entered, then they search the second word in that line to see if it corresponds to the specific mark such as: US, BUG and TK designation, if it does, it saves it under the above date, if it doesn't, it says that there is no valid value entered on the given date, which tells us to an incorrectly entered text or a non-existent record on that date.

All you have to do is add the correct path of the file you want to parse and pass it the output path with the given name of the new file as output

