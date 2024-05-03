double existingRecordInSeconds = double.Parse(Console.ReadLine());
double distanceInMeters = double.Parse(Console.ReadLine());
double timePerMeter = double.Parse(Console.ReadLine());

double totalTimeInSeconds = distanceInMeters * timePerMeter + Math.Floor(distanceInMeters / 15) * 12.5;
if (totalTimeInSeconds < existingRecordInSeconds) Console.WriteLine($"Yes, he succeeded! The new world record is {totalTimeInSeconds:f2} seconds.");
else Console.WriteLine($"No, he failed! He was {totalTimeInSeconds - existingRecordInSeconds:f2} seconds slower.");