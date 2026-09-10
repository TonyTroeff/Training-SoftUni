# Personalized Time Planner

A CLI multi-agent time planner built with LangGraph.

## Run

```bash
pip install -r requirements.txt
cp .env.example .env   # add your OPENAI_API_KEY
python -m time_planner
```

## Usage

```
/today        the agenda for today
/yesterday    the agenda for yesterday
/tomorrow     the agenda for tomorrow
/this-week    the current week as a table: one column per day, entries
              stacked underneath, today's column highlighted
/help         command list
/exit         quit
```

Anything that is not a command is a free-text prompt.
