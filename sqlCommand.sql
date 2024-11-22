select Days,PreviousState,count(*),sum(TimeSpan) from machinestate 
where HappenTime between '2024-11-11 00:33:20' and '2024-11-18 12:33:20' group by Days,PreviousState order by Days;

select Days,PreviousState,count(*),sum(TimeSpan) from machinestate where HappenTime between '2024-11-12' and '2024-11-19 12:33:20' group by Days,PreviousState order by Days


SELECT * FROM machinestate
WHERE HappenTime BETWEEN '2024-11-14 00:00:00' AND '2024-11-14 23:59:59'
ORDER BY HappenTime DESC
LIMIT 1;