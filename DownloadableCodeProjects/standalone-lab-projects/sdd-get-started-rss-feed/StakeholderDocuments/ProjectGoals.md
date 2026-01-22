# Project goals

Build a practical RSS/Atom feed reader with a clean end-to-end architecture and a clear, testable set of requirements. The goal is to deliver a usable first version quickly, then grow capability over time without rewriting the foundation.

## Purpose

The app exists to help a user keep up with content from many sources in one place, without relying on social algorithms. The reader should make it easy to subscribe to feeds, stay on top of new items, and control the flow of information.

## Target scope (today)

This is initially a single-user, local-only application. It is designed to be developed and tested on Windows, macOS, or Linux.

Because the development timeline is intentionally short, we will prioritize an MVP that proves the full workflow (subscribe → fetch → store → read) and defer "power features" until the basics are stable.

Multi-device sync, first-class mobile support, and expanded offline capabilities are intentionally out of scope for the MVP. The initial goal is a reliable local reader, with a clear path to add those capabilities later.

## Delivery approach

We will build in small increments, validating behavior at each step, rather than attempting a large implementation in a single pass.

Each increment should be described in a short, testable way:

- One primary user outcome.
- A handful of acceptance scenarios (a clear success-path plus 2–3 high-value failures).
- The edge cases we are *not* solving yet.

To keep delivery fast and predictable:

- Start with the smallest end-to-end MVP slice first (manual subscribe → refresh → items).
- Treat discovery, background polling, folders, and read/unread as post-MVP unless explicitly pulled in.
- Use a known-good feed for the first success-path verification before testing stricter or rate-limited sources.
- Keep work items small and single-purpose; when a change affects behavior, add/adjust tests first.

When writing a work breakdown (tasks):

- Prefer tasks that touch one file or one concern.
- If a task changes multiple concerns, split it.
- Put test changes first for each behavior change.
- If you use a marker for parallelizable tasks (for example `[P]`), only mark tasks that do not touch the same files.
- Keep local dev ports and base URLs explicit and configurable to avoid unnecessary debugging churn.

## Implementation guardrails

These are "sharp edges" we've identified when working on similar projects. They should be proactively avoided.

- **Solution file**: Ensure the backend solution is `backend/RssFeedReader.sln` (not `.slnx`).
- **Schema initialization**: Do not mix EF Core `Migrate()` and `EnsureCreated()` against the same database; pick one strategy (prefer migrations) and reflect it in integration test setup.
- **SQLite timestamp sorting**: Avoid EF `OrderBy` over `DateTimeOffset` on SQLite unless you've validated translation; prefer UTC `DateTime` for sortable columns or sort in-memory.
- **Dependencies**: If the plan requires HTML sanitization or Swagger, specify the exact package(s) and version policy (including how to handle NU190x vulnerability advisories) so implementation doesn't stall mid-flight.

## What "MVP working" means in practice

For the first MVP slice, "working" should be validated on a known-good RSS/Atom feed with this flow:

1. Add subscription
2. Trigger manual refresh
3. View items

Only after this success-path works reliably should we use stricter sites (rate-limited/blocked sources) as robustness tests.

## Rollout plan

Feature rollout will be staged:

First, we will deliver the MVP: the smallest set of features that provides a functional reader experience end-to-end.

Next, we will iterate toward a solid "v1" by improving usability and reliability (better organization, clearer error handling, and incremental quality improvements) while keeping the system simple.

Finally, we will add optional enhancements over time (search/filtering, integrations, sync, offline improvements) once the core experience is dependable.

## Quality goals for this project

Even in an MVP, the reader should be reliable and safe. It should tolerate real-world feed problems (redirects, timeouts, malformed XML) without crashing, avoid duplicating items unnecessarily, and render content safely.

Local data should remain the user's data. The design should make it easy to keep and export information as the project grows.

## Standards and guidelines

- **Code Quality**: Enforce linters/formatters; follow a clear style guide; require PR reviews; prefer small, incremental changes.
- **Testing**: Unit and integration tests for critical paths; CI gates must run tests; target meaningful coverage on core modules (≈80%) and validate parsing/rendering edge cases.
- **Security**: Sanitize all rendered HTML; validate and normalize inputs; keep secrets out of the repo; run SAST and dependency vulnerability scanning; fix high/critical issues promptly; adopt OWASP ASVS baseline controls.
- **Privacy**: Minimize data collected; keep all user data local by default; make export/delete straightforward; exclude PII from logs.
- **Accessibility**: Meet WCAG 2.2 AA (keyboard navigation, contrast, semantics); avoid motion that impairs readability.
- **Performance**: Non-blocking UI during fetch/parse; cache feed metadata; use retry with backoff on timeouts; avoid excessive memory use on large feeds.
- **Reliability**: Handle malformed/redirected feeds robustly; deduplicate items idempotently; persist safely to avoid corruption.
- **Observability**: Use structured logs with levels; surface actionable error messages; collect minimal opt-in telemetry only.
- **Release Management**: Use semantic versioning; maintain a changelog; ensure reproducible builds; enable quick rollback.
- **Documentation & Process**: Keep README/user guide and architecture overview current; write brief requirements with acceptance criteria; definition of done includes tests, docs, and a short QA checklist.
- **Dependency Management**: Pin versions; update regularly; track licenses; generate an SBOM (e.g., CycloneDX) and verify compliance.

## "MVP first" rule of thumb

If a requirement introduces background scheduling, concurrency orchestration, or cross-cutting UI state (examples: polling, folders, read/unread), it is probably post-MVP unless the app cannot demonstrate the core workflow without it.

## How this document fits with the others

This document describes the project at a high level and sets constraints, methodology, and rollout expectations.

Detailed, user-facing requirements live in [AppFeatures.md](AppFeatures.md).

The technology choices and architectural rationale live in [TechStack.md](TechStack.md).
