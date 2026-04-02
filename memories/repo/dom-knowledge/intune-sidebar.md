# Intune Portal Sidebar  DOM Knowledge
> Component: Left sidebar navigation
> Last captured: 2026-04-02 (from console logs)

## Elements
| Element | Selector | Role | Text | Notes |
|---------|----------|------|------|-------|
| Sidebar Container | `[role='presentation'][class*='fxs-sidebar az-noprint msportalfx-unselectable']` | presentation | - | Main sidebar wrapper |
| Home Link | `[class*='fxs-sidebar-item-link'] >> has-text="Home"` | link | Home | Navigate to home |
| Apps Link | `[class*='fxs-sidebar-item-link'] >> has-text="Apps"` | link | Apps | Navigate to apps section |

## Menu System
| Element | Selector | Role | Notes |
|---------|----------|------|-------|
| Menu Scroll Area | `[class*='fxc-menu-scrollarea']` | navigation | Contains menu items |
| Menu List Item | `[class*='fxc-menu-listView-item']` | menuitem | Individual menu entries |
| All Apps | `[class*='fxc-menu-listView-item'] >> has-text="All apps"` | menuitem | Under Apps menu |

## Behavior
- Sidebar uses `nth=0` indexing for items
- Menu scroll area uses `nth=-1` (last matching)
- Items located via `has-text` patterns (case-insensitive)
