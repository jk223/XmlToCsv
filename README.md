#What was done
- FileManager is created to read and write xml files
- Validation added
- Testcases implemented

#Output files 
- csv files will be written to \Gentrack_JagmeetPOC\Gentrack_JagmeetPOC\bin\Debug

#Assumptions
- It is assumed that lines in CSVIntervalData are separated by newline. More work need to be done if this not true
- Logic can be optimized if the input file is very large, it can be read into multiple splits.
- Implemented thread safe reading and writing in case multiple instances of this application runs.
- If the send element [which is file name] is repeated in any row, it will override existing file. This case can be handled after discussion what need to be done in such situation.
- Unit test covers the validation part mostly and it does not cover the validation of generated csv file. More time is required to implement it.
- Proper patterns are used to scale or enhance this solution later. Algoriths can further be optimized.
- Some comments being written to provide clarity, but proper documentation and proper names of variable, class etc can be further optimized.

#What would be done with more time
- Modify logic to improve performance
