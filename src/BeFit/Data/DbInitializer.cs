using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BeFit.Models;

namespace BeFit.Data
{
    public class DbInitializer
    {
        public static void Initialize(BeFitDbContext context)
        {
            //Look for any Cardio
            if (context.Cardio.Any())
                return; //DB has been seeded
            var cardio = new List<Cardio>
            {
                new Cardio {Name = "Running 6.5 m/h", CalPerHour = 8.9},
                new Cardio {Name = "Running 8 m/h", CalPerHour = 10.3},
                new Cardio {Name = "Elipsoid", CalPerHour = 7.5},
                new Cardio {Name = "Cycling", CalPerHour = 7.7},
                new Cardio {Name = "Aerobics", CalPerHour = 6.5},
                new Cardio {Name = "Dancing", CalPerHour = 6.9},
                new Cardio {Name = "Jumping rope", CalPerHour = 7.7}
            };
            foreach (var car in cardio)
                context.Cardio.Add(car);

            var exercise = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Crunch",
                    Description =
                        "The crunch is one of the most common abdominal exercises. It primarily works the rectus abdominis muscle and also works the obliques./n Lie flat on your back with your feet flat on the ground, or resting on a bench with your knees bent at a 90 degree angle. If you are resting your feet on a bench, place them three to four inches apart and point your toes inward so they touch.Now place your hands lightly on either side of your head keeping your elbows in. Tip: Don't lock your fingers behind your head. While pushing the small of your back down in the floor to better isolate your abdominal muscles, begin to roll your shoulders off the floor. Continue to push down as hard as you can with your lower back as you contract your abdominals and exhale.Your shoulders should come up off the floor only about four inches, and your lower back should remain on the floor. At the top of the movement, contract your abdominals hard and keep the contraction for a second. Tip: Focus on slow, controlled movement - don't cheat yourself by using momentum. After the one second contraction, begin to come down slowly again to the starting position as you inhale.Repeat for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Air Bike",
                    Description =
                        "Lie flat on the floor with your lower back pressed to the ground. For this exercise, you will need to put your hands beside your head. Be careful however to not strain with the neck as you perform it. Now lift your shoulders into the crunch position. Bring knees up to where they are perpendicular to the floor, with your lower legs parallel to the floor. This will be your starting position. Now simultaneously, slowly go through a cycle pedal motion kicking forward with the right leg and bringing in the knee of the left leg. Bring your right elbow close to your left knee by crunching to the side, as you breathe out. Go back to the initial position as you breathe in. Crunch to the opposite side as you cycle your legs and bring closer your left elbow to your right knee and exhale. Continue alternating in this manner until all of the recommended repetitions for each side have been completed."
                },
                new Exercise
                {
                    Name = "Cable Crunch",
                    Description =
                        "Kneel below a high pulley that contains a rope attachment.Grasp cable rope attachment and lower the rope until your hands are placed next to your face. Flex your hips slightly and allow the weight to hyperextend the lower back.This will be your starting position. With the hips stationary, flex the waist as you contract the abs so that the elbows travel towards the middle of the thighs.Exhale as you perform this portion of the movement and hold the contraction for a second. Slowly return to the starting position as you inhale.Tip: Make sure that you keep constant tension on the abs throughout the movement.Also, do not choose a weight so heavy that the lower back handles the brunt of the work. Repeat for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Barbell Guillotine Bench Press",
                    Description =
                        "Using a medium width grip (a grip that creates a 90-degree angle in the middle of the movement between the forearms and the upper arms), lift the bar from the rack and hold it straight over your neck with your arms locked. This will be your starting position.As you breathe in, bring the bar down slowly until it is about 1 inch from your neck. After a second pause, bring the bar back to the starting position as you breathe out and push the bar using your chest muscles.Lock your arms and squeeze your chest in the contracted position, hold for a second and then start coming down slowly again.It should take at least twice as long to go down than to come up. Repeat the movement for the prescribed amount of repetitions. When you are done, place the bar back in the rack."
                },
                new Exercise
                {
                    Name = "Close-Hands Push-Up",
                    Description =
                        "Get in the push-up position on your toes with your hands directly under your shoulders. Your body should be as straight as possible, maintaining the neutral alignment of your head. This is your start position. Allow your elbows to break as you lower your body toward the floor but donâ€™t allow it to touch.Keep your body as straight as possible.Your elbows should be pointing rearward, bent about 90 degrees, at the bottom position. Press back up through your hands to full arm extension. Repeat for the required number of reps."
                },
                new Exercise
                {
                    Name = "Dumbbell Flyes",
                    Description =
                        "Lie down on a flat bench with a dumbbell on each hand resting on top of your thighs. The palms of your hand will be facing each other. Then using your thighs to help raise the dumbbells, lift the dumbbells one at a time so you can hold them in front of you at shoulder width with the palms of your hands facing each other.Raise the dumbbells up like you're pressing them, but stop and hold just before you lock out. This will be your starting position. With a slight bend on your elbows in order to prevent stress at the biceps tendon, lower your arms out at both sides in a wide arc until you feel a stretch on your chest.Breathe in as you perform this portion of the movement. Tip: Keep in mind that throughout the movement, the arms should remain stationary; the movement should only occur at the shoulder joint. Return your arms back to the starting position as you squeeze your chest muscles and breathe out. Tip: Make sure to use the same arc of motion used to lower the weights. Hold for a second at the contracted position and repeat the movement for the prescribed amount of repetitions."
                },
                new Exercise
                {
                    Name = "Alternate Hammer Curl",
                    Description =
                        "Stand up with your torso upright and a dumbbell in each hand being held at arms length. The elbows should be close to the torso. The palms of the hands should be facing your torso. This will be your starting position. While holding the upper arm stationary, curl the right weight forward while contracting the biceps as you breathe out. Continue the movement until your biceps is fully contracted and the dumbbells are at shoulder level. Hold the contracted position for a second as you squeeze the biceps.Tip: Only the forearms should move. Slowly begin to bring the dumbbells back to starting position as your breathe in. Repeat the movement with the left hand.This equals one repetition. Continue alternating in this manner for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Arnold Dumbbell Press",
                    Description =
                        "Sit on an exercise bench with back support and hold two dumbbells in front of you at about upper chest level with your palms facing your body and your elbows bent. Tip: Your arms should be next to your torso. The starting position should look like the contracted portion of a dumbbell curl. Now to perform the movement, raise the dumbbells as you rotate the palms of your hands until they are facing forward. Continue lifting the dumbbells until your arms are extended above you in straight arm position. Breathe out as you perform this portion of the movement. After a second pause at the top, begin to lower the dumbbells to the original position by rotating the palms of your hands towards you. Tip: The left arm will be rotated in a counter clockwise manner while the right one will be rotated clockwise. Breathe in as you perform this portion of the movement. Repeat for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Dips - Triceps Version",
                    Description =
                        "To get into the starting position, hold your body at arm's length with your arms nearly locked above the bars. Now, inhale and slowly lower yourself downward. Your torso should remain upright and your elbows should stay close to your body. This helps to better focus on tricep involvement. Lower yourself until there is a 90 degree angle formed between the upper arm and forearm. Then, exhale and push your torso back up using your triceps to bring your body back to the starting position. Repeat the movement for the prescribed amount of repetitions."
                },
                new Exercise
                {
                    Name = "Bent Over Barbell Row",
                    Description =
                        "Holding a barbell with a pronated grip (palms facing down), bend your knees slightly and bring your torso forward, by bending at the waist, while keeping the back straight until it is almost parallel to the floor. Tip: Make sure that you keep the head up. The barbell should hang directly in front of you as your arms hang perpendicular to the floor and your torso. This is your starting position. Now, while keeping the torso stationary, breathe out and lift the barbell to you. Keep the elbows close to the body and only use the forearms to hold the weight. At the top contracted position, squeeze the back muscles and hold for a brief pause. Then inhale and slowly lower the barbell back to the starting position. Repeat for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Chin-Up",
                    Description =
                        "Grab the pull-up bar with the palms facing your torso and a grip closer than the shoulder width. As you have both arms extended in front of you holding the bar at the chosen grip width, keep your torso as straight as possible while creating a curvature on your lower back and sticking your chest out. This is your starting position. Tip: Keeping the torso as straight as possible maximizes biceps stimulation while minimizing back involvement. As you breathe out, pull your torso up until your head is around the level of the pull - up bar.Concentrate on using the biceps muscles in order to perform the movement.Keep the elbows close to your body.Tip: The upper torso should remain stationary as it moves through space and only the arms should move.The forearms should do no other work other than hold the bar. After a second of squeezing the biceps in the contracted position, slowly lower your torso back to the starting position; when your arms are fully extended. Breathe in as you perform this portion of the movement. Repeat this motion for the prescribed amount of repetitions."
                },
                new Exercise
                {
                    Name = "Barbell Deadlift",
                    Description =
                        "Approach the bar so that it is centered over your feet. Your feet should be about hip-width apart. Bend at the hip to grip the bar at shoulder-width allowing your shoulder blades to protract. Typically, you would use an alternating grip. With your feet and your grip set, take a big breath and then lower your hips and flex the knees until your shins contact the bar. Look forward with your head. Keep your chest up and your back arched, and begin driving through the heels to move the weight upward. After the bar passes the knees aggressively pull the bar back, pulling your shoulder blades together as you drive your hips forward into the bar. Lower the bar by bending at the hips and guiding it to the floor."
                },
                new Exercise
                {
                    Name = "Butt Lift (Bridge)",
                    Description =
                        "Lie flat on the floor on your back with the hands by your side and your knees bent. Your feet should be placed around shoulder width. This will be your starting position. Pushing mainly with your heels, lift your hips off the floor while keeping your back straight.Breathe out as you perform this part of the motion and hold at the top for a second. Slowly go back to the starting position as you breathe in."
                },
                new Exercise
                {
                    Name = "Barbell Full Squat",
                    Description =
                        "This exercise is best performed inside a squat rack for safety purposes. To begin, first set the bar on a rack just above shoulder level. Once the correct height is chosen and the bar is loaded, step under the bar and place the back of your shoulders (slightly below the neck) across it. Hold on to the bar using both arms at each side and lift it off the rack by first pushing with your legs and at the same time straightening your torso. Step away from the rack and position your legs using a shoulder-width medium stance with the toes slightly pointed out. Keep your head up at all times and maintain a straight back. This will be your starting position. Begin to slowly lower the bar by bending the knees and sitting back with your hips as you maintain a straight posture with the head up. Continue down until your hamstrings are on your calves.Inhale as you perform this portion of the movement. Begin to raise the bar as you exhale by pushing the floor with the heel or middle of your foot as you straighten the legs and extend the hips to go back to the starting position. Repeat for the recommended amount of repetitions."
                },
                new Exercise
                {
                    Name = "Jumping Jeck",
                    Description =
                        " Stand with your feet together and your hands down by your side. In one motion jump your feet out to the side and raise your arms above your head. Immediately reverse that motion by jumping back to the starting position."
                }
            };
            foreach (var ex in exercise)
                context.Exercise.Add(ex);

            var muscle = new List<Muscle>
            {
                new Muscle {Name = "Arms"},
                new Muscle {Name = "Back"},
                new Muscle {Name = "Chest"},
                new Muscle {Name = "Abs"},
                new Muscle {Name = "Thigh"}
            };
            foreach (var mus in muscle)
                context.Muscle.Add(mus);

            var foodstuff = new List<Food>();
            // we will read data from file and add to database
            var path = @"C:\Users\Tatsiana\Desktop\foodstufs.txt";
            using (var str = File.OpenText(path))
            {
                double fat = 0;
                double carb = 0;
                var cal = 0;
                double prot = 0;
                string line;

                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = str.ReadLine()) != null)
                {
                    string nam = null;
                    string namsec = null;

                    var comp = line.Split(' ');
                    var count = comp.Length;
                    for (var i = 0; i < count - 4; i++)
                    {
                        if (i == 0)
                            nam = comp[i];
                        else
                            namsec = " " + comp[i];
                        nam = nam + namsec;
                        try
                        {
                            cal = int.Parse(comp[count - 1]);
                            carb = double.Parse(comp[count - 2]);
                            fat = double.Parse(comp[count - 3]);
                            prot = double.Parse(comp[count - 4]);
                        }
                        catch (Exception ex)
                        {
                            // Let the user know what went wrong.
                            Console.WriteLine("The file could not be read:");
                            Console.WriteLine(ex.Message);
                        }
                    }
                    foodstuff.Add(new Food {Calories = cal, Carbohydrate = carb, Fat = fat, Protein = prot, Name = nam});
                }
                foreach (var food in foodstuff)
                    context.Foodstuff.Add(food);
               
                var tags = new List<Tag>
                {
                    new Tag {Name = "Fat Burning"},
                    new Tag {Name = "Strength"},
                    new Tag {Name = "Stretching"}
                };
                foreach (var tag in tags)
                    context.Tag.Add(tag); //add to database
               
                var workout = new List<Workout>
                {
                    new Workout
                    {
                        Name = "Ultimate abs",
                        Description =
                            "A strong abdominal wall affects everything. The way you sit. How you walk. Your performance in every kind of sport. How quickly you get tired and how smoothly you move. This is a workout that presses all the right buttons, helping you tone up and build your abs, plus come summer you're going to be thankful you did it.",
                        TagID = tags.SingleOrDefault(s => s.Name == "Strength").TagID
                    },
                    new Workout
                    {
                        Name = "Killer butt",
                        Description =
                            "The glutes is not a muscle group targeted only by those who want to look good in tight jeans. It is also a mini-powerhouse helping us jump higher, sprint faster and kick harder. They also help us stand up straighter and walk better. The glutes are targeted by the Killer Butt workout, designed specifically to tone and tighten the glutes and create the kind of muscle strength and stability that improves our athletic performance. Add EC and you are really challenging the muscles. ",
                        TagID = tags.SingleOrDefault(s => s.Name == "Strength").TagID
                    },
                    new Workout
                    {
                        Name = "Super Striation Workout",
                        Description =
                            "Looking for a shoulder workout that will leave your shoulders looking ripped from every single angle? Then you need to try the Super Striation Workout!",
                        TagID = tags.SingleOrDefault(s => s.Name == "Strength").TagID
                    },
                    new Workout
                    {
                        Name = "The routine to burn off a carb overload",
                        Description =
                            "The routine to burn off a carb overload .The bodyweight routine you need after gorging on too much Thanksgiving stuffing or a Super Bowl super - binge.",
                        TagID = tags.SingleOrDefault(s => s.Name == "Fat Burning").TagID
                    },
                    new Workout
                    {
                        Name = "The Final Form",
                        Description =
                            "The Final Form is a combat skills workout that works speed, balance, explosiveness, core stability and strength. Keep your side kicks to waist height, make your backfists and punches as fast as possible and really twist fast on the balls of your feet to shift your bodyweight behind your hooks. Don't forget to breathe out explosively as you execute the moves. Add EC and you also challenge your VO2 Max. ",
                        TagID = tags.SingleOrDefault(s => s.Name == "Fat Burning").TagID
                    }
                };
                foreach (var work in workout)
                    context.Workout.Add(work); //add to database
                var measurements = new List<Measurement>
                {
                    new Measurement
                    {
                        Name = "Weight",
                        UnitsOfMeasurement = "kg"
                    },
                    new Measurement
                    {
                        Name = "Chest",
                        UnitsOfMeasurement = "sm"
                    },
                    new Measurement
                    {
                        Name = "Waist",
                        UnitsOfMeasurement = "sm"
                    },
                    new Measurement
                    {
                        Name = "Hips",
                        UnitsOfMeasurement = "sm"
                    },
                };
                foreach (var measurement in measurements)
                {
                    context.Measurement.Add(measurement);
                }

                var fillingworkout = new List<FillingWorkout>
                {
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Crunch").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Ultimate abs").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Air Bike").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Ultimate abs").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Cable Crunch").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Ultimate abs").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Deadlift").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Killer butt").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Butt Lift (Bridge)").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Killer butt").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Full Squat").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Killer butt").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Guillotine Bench Press").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Super Striation Workout").WorkoutID,
                        RepeatMin = 10,
                        RepeatMax = 15,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Chin-Up").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Super Striation Workout").WorkoutID,
                        RepeatMin = 10,
                        RepeatMax = 15,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Dumbbell Flyes").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Super Striation Workout").WorkoutID,
                        RepeatMin = 10,
                        RepeatMax = 15,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Dips - Triceps Version").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "Super Striation Workout").WorkoutID,
                        RepeatMin = 10,
                        RepeatMax = 15,
                        Sets = 3
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Jumping Jeck").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 20,
                        RepeatMax = 30,
                        Sets = 1
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Air Bike").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 2
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Butt Lift (Bridge)").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 2
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Jumping Jeck").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 20,
                        RepeatMax = 30,
                        Sets = 1
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Bent Over Barbell Row").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 1
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Arnold Dumbbell Press").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 2
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Jumping Jeck").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The routine to burn off a carb overload").WorkoutID,
                        RepeatMin = 20,
                        RepeatMax = 30,
                        Sets = 1
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Chin-Up").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The Final Form").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Alternate Hammer Curl").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The Final Form").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Deadlift").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The Final Form").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Cable Crunch").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The Final Form").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    },
                    new FillingWorkout
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Full Squat").ExerciseID,
                        WorkoutID = workout.Single(w => w.Name == "The Final Form").WorkoutID,
                        RepeatMin = 15,
                        RepeatMax = 20,
                        Sets = 4
                    }
                };
                foreach (var work in fillingworkout)
                    context.FillingWorkout.Add(work); //add to database
                var groupofmuscles = new List<GroupsOfMuscles>
                {
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Crunch").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Abs").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Air Bike").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Abs").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Cable Crunch").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Abs").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Deadlift").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Thigh").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Deadlift").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Back").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Chin-Up").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Arms").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Chin-Up").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Chest").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Butt Lift (Bridge)").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Thigh").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Full Squat").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Thigh").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Alternate Hammer Curl").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Arms").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Arnold Dumbbell Press").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Arms").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Arnold Dumbbell Press").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Chest").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Bent Over Barbell Row").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Arms").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Bent Over Barbell Row").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Back").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Dips - Triceps Version").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Arms").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Dumbbell Flyes").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Chest").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Close-Hands Push-Up").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Chest").MuscleID
                    },
                    new GroupsOfMuscles
                    {
                        ExerciseID = exercise.Single(s => s.Name == "Barbell Guillotine Bench Press").ExerciseID,
                        MuscleID = muscle.Single(s => s.Name == "Chest").MuscleID
                    }
                };
                foreach (var group in groupofmuscles)
                    context.GroupsOfMuscles.Add(group);
               
                context.SaveChanges(); //save database
            }
        }
    }
}
