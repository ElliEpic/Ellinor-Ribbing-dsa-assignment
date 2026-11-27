using System;
using System.Diagnostics;
using FrontierDispatch.Core;

if (args.Length > 0 && args[0] == "--baseline")
{
    RunBaseline();
    return;
}

// Small sanity-check application.
// This is separate from the larger performance workload below.
WorldData world = SampleWorldFactory.CreateSmall();
var queries = new WorldQueries(world);

Console.WriteLine("Frontier Dispatch - assignment starter");
Console.WriteLine($"Small demo locations: {world.Locations.Count}");
Console.WriteLine($"Find 734: {queries.FindLocationById(734)?.Name ?? "missing"}");
Console.WriteLine();
Console.WriteLine("Run with --baseline to record the supplied baseline workload.");

static void RunBaseline()
{
    var TimerMediumWorldPrep = Stopwatch.StartNew();
    // Build the dataset before timing the repeated runtime queries.
    // This baseline therefore measures query work, not world construction.
    WorldData world = WorldDatasetFactory.CreateMedium();
    var queries = new WorldQueries(world);
    TimerMediumWorldPrep.Stop();
    // The checksum provides a stable sanity check that the workload still
    // produces equivalent results after implementation changes.
    int checksum = 0;

    const int iterations = 5000;

    var TimerMediumWorldIterate = Stopwatch.StartNew();
    long before = GC.GetAllocatedBytesForCurrentThread();
    for (int i = 0; i < iterations; ++i)
    {
        // Deterministically visit many different location IDs rather than
        // repeatedly querying the same location.
        int id = (i * 997) % world.Locations.Count;

        Location? location = queries.FindLocationById(id);
        checksum += location?.Id ?? 0;
        checksum += queries.FindOutgoingConnections(id).Count;
    }

    TimerMediumWorldIterate.Stop();
    long after = GC.GetAllocatedBytesForCurrentThread();
    long result =  - before;
    
    Console.WriteLine("Baseline world-query workload");
    Console.WriteLine($"Locations: {world.Locations.Count}");
    Console.WriteLine($"Connections: {world.Connections.Count}");
    Console.WriteLine($"Iterations: {iterations}");
    Console.WriteLine($"World Preparation: {TimerMediumWorldIterate.ElapsedMilliseconds} ms");
    Console.WriteLine($"World Iteration: {TimerMediumWorldIterate.ElapsedMilliseconds} ms");
    Console.WriteLine($"Checksum: {checksum}");
    Console.WriteLine("Absolute time varies by machine. Keep this result for before/after comparison.");
    
    
    var TimerLargeWorldPrep = Stopwatch.StartNew();
    // Build the dataset before timing the repeated runtime queries.
    // This baseline therefore measures query work, not world construction.
    long BytesBeforeWorldCreated = GC.GetAllocatedBytesForCurrentThread();
    
    WorldData LargeWorld = WorldDatasetFactory.CreateLarge();
    var LargeQueriequeries = new WorldQueries(world);
    TimerLargeWorldPrep.Stop();
    // The checksum provides a stable sanity check that the workload still
    // produces equivalent results after implementation changes.
    checksum = 0;
    
    var TimerLargeWorldIterations = Stopwatch.StartNew();
    for (int i = 0; i < iterations; ++i)
    {
        // Deterministically visit many different location IDs rather than
        // repeatedly querying the same location.
        int id = (i * 997) % LargeWorld.Locations.Count;

        Location? location = LargeQueriequeries.FindLocationById(id);
        checksum += location?.Id ?? 0;
        checksum += LargeQueriequeries.FindOutgoingConnections(id).Count;
    }

    TimerLargeWorldIterations.Stop();
    long BytesAfterWorldCreatedLarge = GC.GetAllocatedBytesForCurrentThread();
    Console.WriteLine("Large world-query workload");
    Console.WriteLine($"Locations: {LargeWorld.Locations.Count}");
    Console.WriteLine($"Connections: {LargeWorld.Connections.Count}");
    Console.WriteLine($"Iterations: {iterations}");
    Console.WriteLine($"Elapsed1: {TimerLargeWorldPrep.ElapsedMilliseconds} ms");
    Console.WriteLine($"Elapsed2: {TimerLargeWorldIterations.ElapsedMilliseconds} ms");
    Console.WriteLine($"Checksum: {checksum}");
    Console.WriteLine("Absolute time varies by machine. Keep this result for before/after comparison.");
}
