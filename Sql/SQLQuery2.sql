SELECT 
	ROW_NUMBER() OVER(ORDER BY d.NAME) Num,
	d.Name 
FROM 
	Departments d
ORDER BY
	1