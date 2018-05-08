-- included for reference. Used this to obtain data for the unit tests

SELECT E.employee_id AS EmployeeId,
       E.FirstName,
       E.Surname,
       EmployeeShifts =
       (
           SELECT ews.Employee_Id AS 'EmployeeShift.EmployeeId',
                  ews.Shift_Id AS 'EmployeeShift.ShiftId',
                  s.Shift_Id AS 'EmployeeShift.Shift.ShiftId',
                  s.shift_start AS 'EmployeeShift.Shift.ShiftStart',
                  s.shift_end AS 'EmployeeShift.Shift.ShiftEnd',
                  s.shift_name AS 'EmployeeShift.Shift.ShiftName'
           FROM dbo.Employee_Works_Shift ews
               INNER JOIN Shifts s
                   ON ews.shift_id = s.shift_id
           WHERE ews.employee_id = E.employee_id
		   FOR JSON PATH
       )
FROM Employee E
ORDER BY E.employee_id
FOR JSON PATH