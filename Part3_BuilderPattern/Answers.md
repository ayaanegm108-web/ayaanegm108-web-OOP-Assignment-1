# Task 3.1 — Why are 20-parameter constructors a problem?

## Q1: Why is a single 20-parameter constructor a problem in practice?

At the call site, a 20-argument constructor call is just a wall of literals in
parentheses:

```csharp
var invoice = new Invoice("INV-1001", "Mona Ali", "mona@example.com", "010-1111-2222",
    "12 Tahrir St", "Cairo", "Cairo", "11511", "Egypt",
    "45 Corniche Rd", "Alexandria", "Alexandria", "21500", "Egypt",
    new DateTime(2026, 9, 20), "Credit Card", "EGP", 1500m, 150m, 50m, 1400m);
```

Nothing at the call site says which value is which. `"12 Tahrir St"` and
`"45 Corniche Rd"` are both `string`, so the compiler happily accepts them in
whichever slots you put them — if billing and shipping street get swapped, or if
`SubTotal`, `DiscountAmount`, `TaxAmount`, and `TotalAmount` (four `decimal`s in a row)
get reordered, the code compiles and runs, and the bug shows up later as a wrong
invoice total or a package shipped to the wrong address. There's no reviewer-visible
signal, since a diff just shows literals moving around a parameter list.

It also doesn't age well. The day someone needs one more optional property (say,
`PoNumber`), every existing call site with 20 positional arguments either breaks (if
the parameter goes in the middle) or has to append yet another one to an already
unreadable list — and every one of those call sites has to be found and edited.

## Q2: Is this just "the constructor is too long," or something deeper?

It's deeper than constructor length. Putting ~20 loosely related properties on one
class is a **single responsibility** problem: `Invoice` is really three different
concerns glued together — who the customer is, where to bill them, and where to ship
to them, plus the order/payment numbers. Billing and shipping are structurally
*identical* (street/city/state/zip/country) but currently exist as ten independent
flat properties with no shared type, so there's nothing stopping billing-specific
validation logic from silently drifting out of sync with shipping-specific validation
logic, and no way to reuse "validate an address" anywhere else in the codebase.
Shortening the constructor (e.g. with optional parameters or an object initializer)
would fix the readability symptom but not the underlying issue: the class is modeling
several ideas as if they were one. That's exactly what Task 3.3's composed builders
address, by giving the address concept its own type.

---

# Task 3.3 — Why the composed builders are better

**Single responsibility.** `AddressBuilder` owns exactly one thing: assembling a
complete, valid `Address`. `OrderBuilder` owns exactly one thing: assembling a
complete, valid `OrderInfo`. `InvoiceBuilder` (composed version) owns exactly one
thing: combining an already-valid billing address, an already-valid optional shipping
address, and already-valid order info into an `Invoice`. None of the three has to know
how to validate the other two's data.

**Independent validation.** `AddressBuilder.Build()` can refuse to hand back an
`Address` with a blank street or city on its own — `Address`'s constructor enforces
that, with zero knowledge of `Invoice` or of billing vs. shipping. The parent
`InvoiceBuilder` never re-checks "is this a real address"; it just trusts the
`Address` object it was handed, because an invalid one could never have been
constructed in the first place.

**Reuse.** The exact same `AddressBuilder` class builds both the billing and the
shipping address in `Program.cs` — two `.Build()` calls, zero duplicated
street/city/state/zip/country fields or validation. Without it, `InvoiceBuilder`
(Task 3.2's version) has to carry ten separate `With...` methods and five separate
null/blank checks for billing, then repeat all of it again for shipping — any fix to
address validation would need to be applied and tested twice.

**Readability at the call site.** Compare:

```csharp
// Task 3.2 — one big builder, ten address-shaped calls mixed together
new InvoiceBuilder()
    .WithBillingAddress("12 Tahrir St", "Cairo", "Cairo", "11511", "Egypt")
    .WithShippingAddress("45 Corniche Rd", "Alexandria", "Alexandria", "21500", "Egypt")
    ...
```

```csharp
// Task 3.3 — each concept built once, on its own, then handed over
var billing  = new AddressBuilder().WithStreet(...).WithCity(...).Build();
var shipping = new AddressBuilder().WithStreet(...).WithCity(...).Build();
var order    = new OrderBuilder().WithOrderDate(...).WithPayment(...).Build();

new InvoiceBuilder()
    .WithBillingAddress(billing)
    .WithShippingAddress(shipping)
    .WithOrderInfo(order)
    .Build();
```

The composed version reads as "build an address, build another address, build the
order info, then combine them" — each step is small enough to understand (and unit
test) completely on its own, and the final composition step is short because it isn't
also trying to validate five fields' worth of address rules inline.
