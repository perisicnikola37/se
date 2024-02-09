import { VerticalTimeline, VerticalTimelineElement } from "react-vertical-timeline-component";

import "react-vertical-timeline-component/style.min.css";

const ExpenseTrackerTimeline = () => {
  return (
    <VerticalTimeline>
      <div className="mt-48">
        <VerticalTimelineElement
          className="vertical-timeline-element--work"
          date="First step"
          iconStyle={{ background: "#2563EB", color: "#fff" }}
        >
          <h3 className="vertical-timeline-element-title">
            Register on our platform
          </h3>
          <p>
            <a href="/sign-up">Click here</a>{" "}
          </p>
        </VerticalTimelineElement>

        <VerticalTimelineElement
          className="vertical-timeline-element--work"
          date="Second step"
          iconStyle={{ background: "rgb(33, 150, 243)", color: "#fff" }}
        >
          <h3 className="vertical-timeline-element-title">
            Add incomes & expenses
          </h3>
          <hr />
          <a href="">Add income</a>
          <br />
          <a href="">Add expense</a>
        </VerticalTimelineElement>
        <VerticalTimelineElement
          className="vertical-timeline-element--work"
          date="Third step"
          iconStyle={{ background: "#2563EB", color: "#fff" }}
        >
          <h3 className="vertical-timeline-element-title">
            Export your stats and follow chart statistics
          </h3>
          <h4 className="vertical-timeline-element-subtitle">Subtitle</h4>
          <a href="/incomes">Click here</a>
        </VerticalTimelineElement>
      </div>
    </VerticalTimeline>
  );
};

export default ExpenseTrackerTimeline;
