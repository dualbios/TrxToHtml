// // Description :    Definition of CancellationTokenSourceFactory.cs class
// //
// // Copyright © 2025 - 2025, Alcon. All rights reserved.

namespace trxlog2html.Tests;

public class CancellationTokenSourceFactory {
    public static CancellationTokenSource GetCancellationTokenSource() {
#if DEBUG
        return new CancellationTokenSource();
#else
        return new CancellationTokenSource(5000);
#endif
    }
}