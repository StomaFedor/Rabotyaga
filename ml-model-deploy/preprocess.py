import pandas as pd
import joblib
import random


def preprocess(grd: pd.DataFrame):
    ed_type = [4 for i in range(len(grd))]
    id_ = [i for i in range(len(grd))]
    grd['id'] = id_
    grd['education_type'] = ed_type
    grd['age'] = grd['Age'].astype(float)
    grd['experience'] = grd['Experience'].astype(float)
    grd['salary_desired'] = grd['ExpectedSalary'].astype(float)
    grd['pos_topic'] = [2 for i in range(len(grd))]
    gen = grd['Gender']
    gender_code = []
    for i in gen:
        if i == 'Мужчина':
            gender_code.append(1)
        elif i == 'Женщина':
            gender_code.append(0)
        else:
            gender_code.append(2)
    grd['gender'] = gender_code
    grd.drop(['Gender', 'Age', 'Vacation', 'ExpectedSalary', 'YearGraduation', 'Experience'], axis=1, inplace=True)

    grd[['id', 'education_type', 'age', 'experience', 'salary_desired', 'pos_topic', 'gender']]
    grd.sort_values(by=['id', 'age'])

    medians = joblib.load("median_salary_per_age.sav")
    salary = []
    for _, row in grd[['age', 'salary_desired']].iterrows():
        if row['salary_desired'] == 0:
            salary.append(medians[row['age']])
        else:
            salary.append(row['salary_desired'])
    grd['salary_desired'] = salary
    grd.drop(['id'], axis=1, inplace=True)
    return grd
